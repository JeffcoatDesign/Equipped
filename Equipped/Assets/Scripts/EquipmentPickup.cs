using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EquipmentPickup : ItemPickup
{
    private float despawnTime = 20.0f;

    public override void Interact()
    {
        Equip();
    }

    private void Start()
    {
        if (photonView.isRuntimeInstantiated)
        {
            object[] data = photonView.InstantiationData;
            string path = data[0].ToString();
            //Debug.Log(path);
            item = Resources.Load<Item>(path);
            Invoke("Despawn", despawnTime);
        }
        SetSprite();
    }

    void Equip()
    {
        Equipment equipment = (Equipment)item;
        GameUI.instance.clearInteractText();
        EquipmentManager.instance.Equip(equipment);
        photonView.RPC("Remove", photonView.Owner);
    }

    void SetSprite()
    {
        sr.sprite = item.sprite;
    }

    void Despawn()
    {
        photonView.RPC("Remove", photonView.Owner);
    }
    
    [PunRPC]
    void Remove ()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
