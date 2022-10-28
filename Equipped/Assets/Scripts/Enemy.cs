using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Enemy : MonoBehaviourPun
{
    [Header("Info")]
    public string enemyName;
    public float moveSpeed;

    public int curHp;
    public int maxHp;
    public int armor;

    public float chaseRange;
    public float attackRange;

    private PlayerController targetPlayer;

    public float playerDetectRate = 0.2f;
    private float lastPlayerDetectTime;

    [Header("Attack")]
    public int damage;
    public int damageModifier;
    public float attackRate;
    private float lastAttackTime;

    [Header("Components")]
    public HeaderInfo healthBar;
    public SpriteRenderer sr;
    public Rigidbody2D rig;
    public EnemyEquipment enemyEquipment;

    void Start()
    {
        healthBar.Initialize(enemyName, maxHp);
        if (!PhotonNetwork.IsMasterClient)
            return;
        enemyEquipment.PickEquipment();
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if(targetPlayer != null)
        {
            float dist = Vector2.Distance(transform.position, targetPlayer.transform.position);

            if (dist < attackRange && Time.time - lastAttackTime >= attackRate)
                Attack();
            else if (dist > attackRange)
            {
                Vector3 dir = targetPlayer.transform.position - transform.position;
                rig.velocity = dir.normalized * moveSpeed;
            }
            else
            {
                rig.velocity = Vector2.zero;
            }
        }

        DetectPlayer();
    }
    
    //Attacks the target player
    void Attack()
    {
        lastAttackTime = Time.time;
        targetPlayer.photonView.RPC("TakeDamage", targetPlayer.photonPlayer, damage + damageModifier);
    }

    //updates the targeted player
    void DetectPlayer()
    {
        if(Time.time - lastPlayerDetectTime > playerDetectRate)
        {
            lastPlayerDetectTime = Time.time;

            //loop through all players
            foreach(PlayerController player in GameManager.instance.players)
            {
                if (player == null)
                    continue;
                //calc the distance 
                float dist = Vector2.Distance(transform.position, player.transform.position);

                if(player == targetPlayer)
                {
                    if (dist > chaseRange)
                        targetPlayer = null;
                }
                else if(dist < chaseRange)
                {
                    if (targetPlayer == null)
                        targetPlayer = player;
                }
            }
        }
    }

    [PunRPC]
    public void TakeDamage (int damage)
    {
        curHp -= Mathf.Clamp(damage - armor, 0, damage);

        healthBar.photonView.RPC("UpdateHealthBar", RpcTarget.All, curHp);

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
        enemyEquipment.DropRandomLoot();

        // destroy across network
        PhotonNetwork.Destroy(gameObject);
    }
}
