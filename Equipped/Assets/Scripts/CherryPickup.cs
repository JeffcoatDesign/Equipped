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
        PhotonNetwork.Destroy(gameObject);
    }
}
