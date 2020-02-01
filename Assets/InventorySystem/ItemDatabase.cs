using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase _instance;
    public List<Item> items;
    // Start is called before the first frame update
    public ItemDatabase getInstance()
    {
        return _instance;
    }
    public List<Item> getItems()
    {
        return _instance.items;
    }
}
