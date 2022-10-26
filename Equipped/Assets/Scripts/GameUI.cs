using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI cherryText;

    public static GameUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateCherryText(Inventory.instance.cherryCount);
    }

    public void UpdateGoldText (int gold)
    {
        goldText.text = "<b>Gold:</b> " + gold;
    }

    public void UpdateCherryText (int cherries)
    {
        cherryText.text = "" + cherries;
    }

    public void SetInteractText (string str)
    {
        interactText.text = str;
    }
    public void clearInteractText ()
    {
        interactText.text = "";
    }
}
