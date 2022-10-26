using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int cherryCount;
    public Cherry cherryItem;

    #region Singleton
    //instance
    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public void Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Add(item);

            if(OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public void AddCherry ()
    {
        cherryCount++;
        //update game ui
        GameUI.instance.UpdateCherryText(cherryCount);
    }

    public void UseCherry ()
    {
        cherryCount--;
        GameUI.instance.UpdateCherryText(cherryCount);
    }
}
