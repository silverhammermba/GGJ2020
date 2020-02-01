using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    public List<int> currentState;
    public Recipe recipe;
    public bool checkState()
    {
        if (currentState == recipe.required_items)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<int> getRequiredItems()
    {
        return recipe.required_items.Except(currentState).ToList();
    }
    public bool repairObject(Item i)
    {
        int item_id = Int32.Parse(i.id);
        if (getRequiredItems().Contains(item_id))
        {
            currentState.Sort();
            currentState.Add(item_id);
            return true;
        }
        else
        {
            return false;
        }
    }
}
