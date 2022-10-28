using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyEquipment : MonoBehaviourPun
{
    public Enemy enemy;
    public List<SpriteRenderer> spriteRenderers;
    public Equipment[] currentEquipment;
    public string equipmentPrefabPath;
    public Vector3 dropOffset;
    public LootTable loot;

    public void PickEquipment ()
    {
        Equip(loot.skins[Random.Range(0, loot.skins.Length)]);
        Equip(loot.armors[Random.Range(0, loot.armors.Length)]);
        Equip(loot.pants[Random.Range(0, loot.pants.Length)]);
        Equip(loot.shoes[Random.Range(0, loot.shoes.Length)]);
        Equip(loot.weapons[Random.Range(0, loot.weapons.Length)]);
        Equip(loot.hairs[Random.Range(0, loot.hairs.Length)]);

        if (Random.Range(0, 100) <= loot.hasHatChance)
            Equip(loot.hats[Random.Range(0, loot.hats.Length)]);
        else
            Equip(loot.defHat);

        if (Random.Range(0, 100) <= loot.hasHatChance)
            Equip(loot.shields[Random.Range(0, loot.shields.Length)]);
        else
            Equip(loot.defShield);
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = ((int)newItem.equipSlot);
        if (currentEquipment[slotIndex] != null && !currentEquipment[slotIndex].isDefaultItem)
        {
            enemy.armor -= currentEquipment[slotIndex].armorModifier;
            enemy.damageModifier -= currentEquipment[slotIndex].damageModifier;
        }
        //Debug.Log(slotIndex);
        currentEquipment[slotIndex] = newItem;
        photonView.RPC("SetSprite", RpcTarget.AllBuffered, newItem.itemPath, slotIndex);
        enemy.armor += newItem.armorModifier;
        enemy.damageModifier += newItem.damageModifier;
    }

    public void DropRandomLoot()
    {
        if (Random.Range(0, 100) <= loot.gearDropChance)
        {
            Equipment chosenEquipment = null;
            while (chosenEquipment == null)
            {
                int randIndex = Random.Range(0, 4);
                if (randIndex == 0 && !currentEquipment[0].isDefaultItem)
                    chosenEquipment = currentEquipment[0];
                else if (randIndex == 1 && !currentEquipment[1].isDefaultItem)
                    chosenEquipment = currentEquipment[1];
                else if (randIndex == 2 && !currentEquipment[3].isDefaultItem)
                    chosenEquipment = currentEquipment[3];
                else if (randIndex == 3 && !currentEquipment[4].isDefaultItem)
                    chosenEquipment = currentEquipment[4];
            }
            DropEquipment(chosenEquipment);
        }
        else if (Random.Range(1, 100) <= loot.cherryChance)
            DropCherry(loot.cherry);
        else
            DropTreasure(loot.treasures[Random.Range(0, loot.treasures.Length)]);
    }

    void DropCherry(Cherry cherry)
    {
        object[] instanceData = new object[1];
        instanceData[0] = cherry.itemPath;
        PhotonNetwork.Instantiate(loot.cherryPath, transform.position + dropOffset, Quaternion.identity, 0, instanceData);
    }

    void DropTreasure(Treasure treasure)
    {
        object[] instanceData = new object[1];
        instanceData[0] = treasure.itemPath;
        PhotonNetwork.Instantiate(loot.treasurePath, transform.position + dropOffset, Quaternion.identity, 0, instanceData);
    }

    void DropEquipment(Equipment cur)
    {
        if (cur.isDefaultItem)
            return;
        object[] instanceData = new object[1];
        instanceData[0] = cur.itemPath;
        PhotonNetwork.Instantiate(equipmentPrefabPath, transform.position + dropOffset, Quaternion.identity, 0, instanceData);
    }

    //{Head, Chest, Pants, Weapon, Shield, Shoes, Hair, Skin}
    [PunRPC]
    public void SetSprite(string equipPath, int equipmentIndex)
    {
        spriteRenderers[equipmentIndex].sprite = Resources.Load<Equipment>(equipPath).sprite;
    }
}
