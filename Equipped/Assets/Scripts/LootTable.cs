using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "LootTables/LootTable")]
public class LootTable : ScriptableObject
{
    public Treasure[] treasures;
    public Cherry cherry;
    public string treasurePath;
    public string cherryPath;

    //{Head, Chest, Pants, Weapon, Shield, Shoes, Hair, Skin}
    public Equipment[] hats;
    public Equipment[] armors;
    public Equipment[] pants;
    public Equipment[] weapons;
    public Equipment[] shields;
    public Equipment[] shoes;
    public Equipment[] hairs;
    public Equipment[] skins;
    public Equipment defHat;
    public Equipment defShield;

    public int hasHatChance;
    public int hasShieldChance;

    public int gearDropChance;
    public int cherryChance;
}
