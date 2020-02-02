using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Repairable : Interactable
{
    public List<int> currentState;
    public UIManager um;
    public Recipe recipe;
    public bool isFinished;
    public RecipeDatabase rd;
    public int recipe_id;
    public string reward;
    public GameObject affectedobj;
    public CallbackLibrary cb;
    private Animator reward_anim;
    public bool hasAnim;
    public void Start()
    {
        if (this.hasAnim)
        {
            reward_anim = this.gameObject.GetComponent<Animator>();
        }
        isFinished = false;
        cb = CallbackLibrary.Instance;
        rd = RecipeDatabase.Instance;
        Debug.LogWarning(rd.getRecipeFromId(recipe_id).repair_anim_string.ToString());
        this.recipe = rd.getRecipeFromId(recipe_id);
        this.recipe.name = rd.getRecipeFromId(recipe_id).name;
        this.recipe.required_items = rd.getRecipeFromId(recipe_id).required_items;
        this.recipe.reward_func_string = rd.getRecipeFromId(recipe_id).reward_func_string;
        this.recipe.repair_anim_string = rd.getRecipeFromId(recipe_id).repair_anim_string;
        this.recipe.richText_description = rd.getRecipeFromId(recipe_id).richText_description;
    }
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
    public override void interact()
    {
        if (um.isToolTipEnabled())
        {
            um.disableToolTip();
        }
        else
        {
            um.clearTooltip(this);
            um.enabeTooltip(this);
            um.populateTooltip(this);
        }
    }
    public List<int> getRequiredItems()
    {
        //return recipe.required_items.Except(currentState).ToList();
        var lookup2 = this.currentState.ToLookup(str => str);

        var result = from str in recipe.required_items
                     group str by str into strGroup
                     let missingCount
                          = Math.Max(0, strGroup.Count() - lookup2[strGroup.Key].Count())
                     from missingStr in strGroup.Take(missingCount)
                     select missingStr;
        return result.ToList();
    }
    public bool isDone()
    {
        this.currentState.Sort();
        this.recipe.required_items.Sort();
        if (this.currentState.SequenceEqual(this.recipe.required_items))
        {
            Debug.LogWarning("Repair complete!");
            Debug.LogWarning("Invoking callback" + this.recipe.reward_func_string);
            
            //affectedobj.SendMessage(reward);
            this.isFinished = true;
            if (this.hasAnim)
            {
                Debug.Log("Playing finished anim: " + this.recipe.repair_anim_string);
                reward_anim.Play(rd.getRecipeFromId(recipe_id).repair_anim_string);
            }
            cb.Invoke(rd.getRecipeFromId(recipe_id).reward_func_string, 1f);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void printArr(List<int> ig, string l)
    {
        String s = "";
        foreach(int i in ig)
        {
            s = s + i.ToString() + ", ";
        }
        Debug.LogWarning(l + s);
    }
    public bool repairObject(Item i)
    {
        this.currentState.Sort();
        this.recipe.required_items.Sort();
        if (this.currentState.Equals(this.recipe.required_items))
        {
            um.disableToolTip();
            return false;
        }
        int item_id = Int32.Parse(i.id);
        if (getRequiredItems().Contains(item_id))
        {
            for(int z = 0; z < getRequiredItems().Count; z++){
                Debug.Log(getRequiredItems()[z]);
            }
            currentState.Sort();
            currentState.Add(item_id);
            um.populateTooltip(this);
            if(this.currentState.SequenceEqual(this.recipe.required_items)) 
            {
                this.isFinished = true;
                um.EnableRepairText();
            }
            else 
            {
                um.DisableRepairText();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
