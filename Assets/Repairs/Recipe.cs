using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class Recipe
{
    public int id;
    public string name;
    public List<int> required_items;
    public string reward_func_string;
    public string repair_anim_string;
    public string richText_description;
    public Recipe()
    {

    }
}
