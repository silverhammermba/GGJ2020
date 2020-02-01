using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase : MonoBehaviour
{
    //TODO: private
    public static RecipeDatabase instance;
    public List<Recipe> recipes;
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
