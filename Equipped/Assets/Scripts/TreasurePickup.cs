using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TreasurePickup : ItemPickup
{
    public override void Interact()
    {
        AddGold();
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

    void AddGold()
    {
        GameUI.instance.clearInteractText();
        GameManager.instance.localPlayer.GiveGold(((Treasure)item).goldAmount);
        photonView.RPC("Remove", photonView.Owner);
    }

    void SetSprite()
    {
        sr.sprite = item.sprite;
    }

    [PunRPC]
    void Remove()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
