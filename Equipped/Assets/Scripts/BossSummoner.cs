using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossSummoner : Purchasable
{
    public string bossPrefabPath;
    public Vector3 offset;
    public override void Purchase()
    {
        Summon();
    }

    void Summon ()
    {
        ChatBox.instance.photonView.RPC("Log", RpcTarget.All, "Game", "The Boss has awoken!!!");
        photonView.RPC("SpawnBoss", PhotonNetwork.MasterClient);
    }

    [PunRPC]
    void SpawnBoss ()
    {
        PhotonNetwork.Instantiate(bossPrefabPath, transform.position + offset, Quaternion.identity);
    }
}

