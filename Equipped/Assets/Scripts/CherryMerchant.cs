using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CherryMerchant : Purchasable
{
    public override void Purchase()
    {
        Inventory.instance.AddCherry();
    }
}

