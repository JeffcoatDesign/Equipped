using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CherryPickup : ItemPickup
{
    public override void Interact()
    {
        GetCherry();
    }

    void GetCherry ()
    {
        GameUI.instance.clearInteractText();
        Inventory.instance.AddCherry();
        photonView.RPC("Remove", photonView.Owner);
    }

    [PunRPC]
    void Remove ()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
