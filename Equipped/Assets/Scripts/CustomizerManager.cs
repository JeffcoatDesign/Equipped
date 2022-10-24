using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizerManager : MonoBehaviour
{
    public Sprite shirt;
    public Sprite skin;
    public Sprite hair;
    public Sprite shoe;
    public Sprite pants;

    //instance
    public static CustomizerManager instance;

    void Awake()
    {
        instance = this;
    }
}
