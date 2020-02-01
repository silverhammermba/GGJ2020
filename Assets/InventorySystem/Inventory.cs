using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory;
    public int maxSize = 5;
    public Inventory()
    {
        this.inventory = new List<Item>();
    }
    public int getEmptySlot()
    {
        if (inventory.Count < maxSize)
        {
            return inventory.Count + 1;
        }
        else
        {
            return 99;
        }
    }
    public void addItem(Item i)
    {
      if (inventory.Contains(i))
        {
            if (i.isStackable)
            {
                int ind = inventory.IndexOf(i);
                inventory[ind].stackSize = inventory[ind].stackSize + 1;

            }
        }
      if (inventory.Count == maxSize)
        {
            Debug.Log("Inventory is full");
        }
        else
        {
            inventory.Add(i);
        }
    }
    public void removeItem(int index)
    {
        inventory.RemoveAt(index);
    }
}