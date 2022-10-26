using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;
    public string equipmentPrefabPath;
    public Vector3 dropOffset;

    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = ((int)newItem.equipSlot);

        if (currentEquipment[slotIndex] != null && !currentEquipment[slotIndex].isDefaultItem)
        {
            DropEquipment(currentEquipment[slotIndex]);
            GameManager.instance.localPlayer.armor -= currentEquipment[slotIndex].armorModifier;
            GameManager.instance.localPlayer.damageModifier -= currentEquipment[slotIndex].damageModifier;
        }
        //Debug.Log(slotIndex);
        currentEquipment[slotIndex] = newItem;
        GameManager.instance.localPlayer.playerEquipment.photonView.RPC("SetSprite", RpcTarget.All, newItem.itemPath, slotIndex);
        GameManager.instance.localPlayer.armor += newItem.armorModifier;
        GameManager.instance.localPlayer.damageModifier += newItem.damageModifier;
    }

    public void DropEquipment (Equipment cur)
    {
        object[] instanceData = new object[1];
        instanceData[0] = cur.itemPath;
        Vector3 playerPosition = GameManager.instance.localPlayer.transform.position;
        PhotonNetwork.Instantiate(equipmentPrefabPath, playerPosition + dropOffset, Quaternion.identity, 0, instanceData);
    }
}
