using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI interactText;

    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateGoldText (int gold)
    {
        goldText.text = "<b>Gold:</b> " + gold;
    }

    public void SetInteractText (string interaction = "Interact", string obj = "Object")
    {
        interactText.text = "Press [E] to " + interaction + " " + obj;
    }
    public void clearInteractText ()
    {
        interactText.text = "";
    }
}
