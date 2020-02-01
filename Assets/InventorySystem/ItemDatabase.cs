using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemDatabase : MonoBehaviour
{
    //TODO: private
    public static ItemDatabase instance;
    public List<Item> items;
    public static ItemDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemDatabase>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(ItemDatabase).Name;
                    instance = obj.AddComponent<ItemDatabase>();
                }
            }
            return instance;
        }
    }
}