using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizerManager : MonoBehaviour
{
    public SpriteRenderer[] srList;

    public Equipment[] shirts;
    public Equipment[] pants;
    public Equipment[] shoes;
    public Equipment[] hair;
    public Equipment[] skins;

    public Equipment defaultWeapon;
    public Equipment defaultHat;
    public Equipment defaultShield;

    public Slider skinSlider;
    public Slider pantsSlider;
    public Slider shoesSlider;
    public Slider hairSlider;
    public Slider shirtSlider;

    public Equipment[] selections = new Equipment[8];

    public static CustomizerManager instance;

    private void Start()
    {
        instance = this;

        SetSprite(shirts[0]);
        SetSprite(pants[0]);
        SetSprite(hair[0]);
        SetSprite(shoes[0]);
        SetSprite(skins[0]);

        SetSprite(defaultWeapon);
        SetSprite(defaultHat);
        SetSprite(defaultShield);

        skinSlider.maxValue = skins.Length - 1;
        shirtSlider.maxValue = shirts.Length - 1;
        shoesSlider.maxValue = shoes.Length - 1;
        pantsSlider.maxValue = pants.Length - 1;
        hairSlider.maxValue = hair.Length - 1;
    }

    public void OnChangeHait()
    {
        int indexValue = (int)hairSlider.value;
        SetSprite(hair[indexValue]);
    }

    public void OnChangeShirt()
    {
        int indexValue = (int)shirtSlider.value;
        SetSprite(shirts[indexValue]);
    }

    public void OnChangePants()
    {
        int indexValue = (int)pantsSlider.value;
        SetSprite(pants[indexValue]);
    }

    public void OnChangeShoes()
    {
        int indexValue = (int)shoesSlider.value;
        SetSprite(shoes[indexValue]);
    }

    public void OnChangeSkin()
    {
        int indexValue = (int)skinSlider.value;
        SetSprite(skins[indexValue]);
    }

    void SetSprite(Equipment equipment)
    {
        int slotIndex = (int)equipment.equipSlot;
        selections[slotIndex] = equipment;
        srList[slotIndex].sprite = equipment.sprite;
    }
}