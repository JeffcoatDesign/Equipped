using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EquipmentPickup : ItemPickup
{
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
        }
        SetSprite();
    }

    void Equip()
    {
        Equipment equipment = (Equipment)item;
        GameUI.instance.clearInteractText();
        EquipmentManager.instance.Equip(equipment);
        PhotonNetwork.Destroy(gameObject);
    }

    void SetSprite()
    {
        sr.sprite = item.sprite;
    }
}
