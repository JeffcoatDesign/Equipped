using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizer : MonoBehaviour
{
    [Header("Sliders")]
    public Slider shirtSlider;
    public Slider shoeSlider;
    public Slider pantsSlider;
    public Slider skinSlider;
    public Slider hairSlider;
    [Header("Sprites")]
    public List<Sprite> shirtList;
    private int shirtIndex = 0;
    public List<Sprite> shoeList;
    private int shoeIndex = 0;
    public List<Sprite> pantsList;
    private int pantsIndex = 0;
    public List<Sprite> skinList;
    private int skinIndex = 0;
    public List<Sprite> hairList;
    private int hairIndex = 0;
    [Header("Sprite Components")]
    public SpriteRenderer shirtSR;
    public SpriteRenderer shoeSR;
    public SpriteRenderer pantsSR;
    public SpriteRenderer skinSR;
    public SpriteRenderer hairSR;

    void Start()
    {
        shirtSlider.maxValue = shirtList.Count;
        shoeSlider.maxValue = shoeList.Count;
        pantsSlider.maxValue = pantsList.Count;
        skinSlider.maxValue = skinList.Count;
        hairSlider.maxValue = hairList.Count;

        shirtSR.sprite = shirtList[0];
        shoeSR.sprite = shoeList[0];
        pantsSR.sprite = pantsList[0];
        skinSR.sprite = skinList[0];
        hairSR.sprite = hairList[0];

        CustomizerManager.instance.shirt = shirtSR.sprite;
        CustomizerManager.instance.shoe = shoeSR.sprite;
        CustomizerManager.instance.pants = pantsSR.sprite;
        CustomizerManager.instance.skin = skinSR.sprite;
        CustomizerManager.instance.hair = hairSR.sprite;
    }

    public void UpdateCharacter ()
    {
        shirtIndex = (int)shirtSlider.value - 1;
        shoeIndex = (int)shoeSlider.value - 1;
        pantsIndex = (int)pantsSlider.value - 1;
        skinIndex = (int)skinSlider.value - 1;
        hairIndex = (int)hairSlider.value - 1;

        shirtSR.sprite = shirtList[shirtIndex];
        shoeSR.sprite = shoeList[shoeIndex];
        pantsSR.sprite = pantsList[pantsIndex];
        skinSR.sprite = skinList[skinIndex];
        hairSR.sprite = hairList[hairIndex];

        CustomizerManager.instance.shirt = shirtSR.sprite;
        CustomizerManager.instance.shoe = shoeSR.sprite;
        CustomizerManager.instance.pants = pantsSR.sprite;
        CustomizerManager.instance.skin = skinSR.sprite;
        CustomizerManager.instance.hair = hairSR.sprite;
    }
}
