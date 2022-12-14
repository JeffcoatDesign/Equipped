using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public int gold;
    public int curHp;
    public int maxHp;
    public bool dead;
    public int armor;
    public float interactionRadius;

    [Header("Attack")]
    public int damage;
    public int damageModifier;
    public float attackRange;
    public float attackRate;
    private float lastAttackTime;

    [Header("Components")]
    public Rigidbody2D rig;
    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator weaponAnim;
    public HeaderInfo headerInfo;
    public PlayerEquipment playerEquipment;

    [Header("Interaction")]
    public Interactable focusedInteract;

    //local player
    public static PlayerController me;

    [PunRPC]
    public void Initialize (Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;

        headerInfo.Initialize(player.NickName, maxHp);

        if (player.IsLocal)
        {
            me = this;
            GameManager.instance.localPlayer = this;
            //set default appearance
            playerEquipment.photonView.RPC("SetSprite", RpcTarget.AllBuffered, CustomizerManager.instance.selections[1].itemPath, 1);
            playerEquipment.photonView.RPC("SetSprite", RpcTarget.AllBuffered, CustomizerManager.instance.selections[2].itemPath, 2);
            playerEquipment.photonView.RPC("SetSprite", RpcTarget.AllBuffered, CustomizerManager.instance.selections[5].itemPath, 5);
            playerEquipment.photonView.RPC("SetSprite", RpcTarget.AllBuffered, CustomizerManager.instance.selections[6].itemPath, 6);
            playerEquipment.photonView.RPC("SetSprite", RpcTarget.AllBuffered, CustomizerManager.instance.selections[7].itemPath, 7);
        }
        else
            rig.isKinematic = true;
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;

        CheckForInteractable();
        TryCherry();

        Move();

        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackRate)
            Attack();

        float mouseX = (Screen.width / 2) - Input.mousePosition.x;

        if (mouseX < 0)
            weaponAnim.transform.parent.localScale = new Vector3(1, 1, 1);
        else
            weaponAnim.transform.parent.localScale = new Vector3(-1, 1, 1);
    }

    void Move ()
    {
        // get inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(x, y) * moveSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    void CheckForInteractable ()
    {
        RaycastHit2D hit = Physics2D.CircleCast(rig.position, interactionRadius, transform.right, 0f, 1 << LayerMask.NameToLayer("Interactable"));
        if (hit.collider == null)
        {
            RemoveFocus();
            return;
        }
        //Debug.Log(hit.collider.name);
        Interactable interactable = hit.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
            SetFocus(interactable);
        }
        else
            RemoveFocus();
    }

    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focusedInteract)
        {
            if(focusedInteract != null)
                focusedInteract.OnDefocused();

            focusedInteract = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focusedInteract != null)
            focusedInteract.OnDefocused();

        focusedInteract = null;
        GameUI.instance.clearInteractText();
    }

    void TryCherry ()
    {
        if (Inventory.instance.cherryCount > 0 && Input.GetKeyDown(KeyCode.F))
        {
            Inventory.instance.UseCherry();
            photonView.RPC("Heal", photonPlayer, Inventory.instance.cherryItem.healAmount);
        }
    }

    void Attack ()
    {
        lastAttackTime = Time.time;

        Vector3 dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, attackRange);

        if(hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            //get the enemy and damage them
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy.photonView.RPC("TakeDamage", RpcTarget.MasterClient, damage + damageModifier);
        }

        weaponAnim.SetTrigger("Attack");
    }

    [PunRPC]
    public void TakeDamage (int damage)
    {
        curHp -= Mathf.Clamp(damage - armor, 0, damage);

        //update healthbar
        headerInfo.photonView.RPC("UpdateHealthBar", RpcTarget.All, curHp);

        if (curHp <= 0)
            Die();
        else
        {
            photonView.RPC("FlashDamage", RpcTarget.All);
        }
    }

    [PunRPC]
    void FlashDamage ()
    {
        StartCoroutine(DamageFlash());

        IEnumerator DamageFlash()
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            sr.color = Color.white;
        }
    }

    void Die ()
    {
        dead = true;
        rig.isKinematic = true;

        transform.position = new Vector3(0, 99, 0);

        Vector3 spawnPos = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Length)].position;
        StartCoroutine(Spawn(spawnPos, GameManager.instance.respawnTime));
    }

    IEnumerator Spawn (Vector3 spawnPos, float timeToSpawn)
    {
        yield return new WaitForSeconds(timeToSpawn);

        dead = false;
        transform.position = spawnPos;
        curHp = maxHp;
        rig.isKinematic = false;

        // update health bar
        headerInfo.photonView.RPC("UpdateHealthBar", RpcTarget.All, curHp);
    }

    [PunRPC]
    void Heal (int amountToHeal)
    {
        curHp = Mathf.Clamp(curHp + amountToHeal, 0, maxHp);

        //Update health bar
        headerInfo.photonView.RPC("UpdateHealthBar", RpcTarget.All, curHp);
    }

    [PunRPC]
    public void GiveGold (int goldToGive)
    {
        gold += goldToGive;
        //update ui
        GameUI.instance.UpdateGoldText(gold);
    }
}
