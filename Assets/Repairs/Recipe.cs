using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class Recipe
{
    public int id;
    public List<int> required_items;
    public string reward_func_string;

    public Recipe()
    {

    }
}
