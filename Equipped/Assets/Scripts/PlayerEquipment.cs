using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerEquipment : MonoBehaviourPun
{
    public List<SpriteRenderer> spriteRenderers;

    //{Head, Chest, Pants, Weapon, Shield, Shoes, Hair, Skin}
    [PunRPC]
    public void SetSprite (string equipPath, int equipmentIndex)
    {
        spriteRenderers[equipmentIndex].sprite = Resources.Load<Equipment>(equipPath).sprite;
    }
}
