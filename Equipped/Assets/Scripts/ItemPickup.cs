using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        PickUp();
    }

    void PickUp ()
    {
        GameUI.instance.clearInteractText();
        Debug.Log("Picking up" + item.name);
        Destroy(gameObject);
    }
}
