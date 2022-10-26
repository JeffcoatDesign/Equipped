using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemPickup : Interactable
{
    public SpriteRenderer sr;
    public Item item;

    void Start()
    {
        if(item.sprite != null)
            sr.sprite = item.sprite;
    }

    public override void Interact()
    {
        PickUp();
    }

    public override void setText()
    {
        if (item.name != null)
            GameUI.instance.SetInteractText("Press [" + interactKey.ToString() + "] to " + interactionVerb + " " + item.name);
    }

    void PickUp ()
    {
        GameUI.instance.clearInteractText();
        //Debug.Log("Picking up " + item.name);
        Inventory.instance.Add(item);
        photonView.RPC("Remove", photonView.Owner);
    }

    [PunRPC]
    void Remove ()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
