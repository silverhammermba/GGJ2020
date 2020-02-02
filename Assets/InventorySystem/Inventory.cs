    using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory;
    public int maxSize = 5;
    public InventoryGUIController igc;
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
    public bool addItem(Item i)
    {
 
        if (inventory.Count == maxSize)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        else
        {
            if (i.isStackable)
            {
                if (inventory.Contains(i))
                {
                    int ind = inventory.IndexOf(i);
                    inventory[ind].stackSize = inventory[ind].stackSize + 1;
                    igc.refresh();
                    return true;
                }
                else
                {
                    inventory.Add(i);
                    igc.refresh();
                    return true;
                }
            }
            else
            {
                inventory.Add(i);
                igc.refresh();
                return true;
            }
        }
    }
    public void removeItem(int index)
    {
        if (inventory[index].isStackable)
        {
            if (inventory[index].stackSize > 1)
            {
                inventory[index].stackSize = inventory[index].stackSize - 1;
                igc.refresh();
            }
            else
            {
                inventory.RemoveAt(index);
                igc.refresh();
            }
        }
        else
        {
            inventory.RemoveAt(index);
            igc.refresh();
        }
    }
}