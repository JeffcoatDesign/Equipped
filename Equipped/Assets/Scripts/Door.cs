using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Door : Purchasable
{
    public override void Purchase()
    {
        photonView.RPC("OpenDoor", RpcTarget.MasterClient);
    }

    [PunRPC]
    void OpenDoor ()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }
}

