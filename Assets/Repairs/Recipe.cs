using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Recipe
{
    public List<int> required_items;
    public delegate void rewardFunction();
    public rewardFunction rf;
    public void rewardFunc()
    {
        this.rf = rewardFunc;
    }

}
