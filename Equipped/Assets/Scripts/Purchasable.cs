using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : Interactable
{
    public int price;
    PlayerController player;

    public override void setText()
    {
        GameUI.instance.SetInteractText("[" + interactKey.ToString() + "] Pay " + price + " To " + interactionVerb);
    }

    public override void Interact()
    {
        player = GameManager.instance.localPlayer;

        if (player.gold >= price)
        {
            player.gold -= price;
            GameUI.instance.UpdateGoldText(player.gold);
            Purchase();
        }
    }

    public virtual void Purchase ()
    {
        Debug.Log("Bought" + name);
    }
}
