using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    public string id;
    public string name;
    public string Description;
    public bool isStackable;
    public int stackSize;
    public Sprite icon;
    public Item()
    {

    }
   public Item(Item i)
    {
        this.id = i.id;
        this.name = i.name;
        this.Description = i.Description;
        this.isStackable = i.isStackable;
        this.icon = i.icon;
    }
    public Item(string _id, string _name, string _desc, bool _isstack, Sprite _icon)
    {
        this.id = _id;
        this.name = _name;
        this.Description = _desc;
        this.isStackable = _isstack;
        this.icon = _icon;
    }
}
