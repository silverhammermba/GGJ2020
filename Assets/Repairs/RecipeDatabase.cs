using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase : MonoBehaviour
{
    //TODO: private
    public static RecipeDatabase instance;
    public List<Recipe> recipes;
    public Recipe getRecipeFromId(int id)
    {
        Recipe re = new Recipe();
        foreach (Recipe i in recipes)
        {
            if (i.id == id)
            {
                Debug.LogWarning("Found recipe");
                re.id = i.id;
                re.required_items = i.required_items;
                re.reward_func_string = i.reward_func_string;
            }
        }
        return re;
    }
    public static RecipeDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RecipeDatabase>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(RecipeDatabase).Name;
                    instance = obj.AddComponent<RecipeDatabase>();
                }
            }
            return instance;
        }
    }
}
