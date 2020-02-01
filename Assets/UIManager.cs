using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    public Canvas mainUi;
    public RepairTooltip rtp;
    public Sprite slotFilled;
    public Sprite slotEmpty;
    public ItemDatabase _id;
    public void Start()
    {
        _id = ItemDatabase.Instance;
    }
    public void enabeTooltip()
    {
        rtp.gameObject.SetActive(true);
    }
    public void disableToolTip()
    {
        rtp.gameObject.SetActive(false);
    }
    public bool isToolTipEnabled()
    {
        if (rtp.gameObject.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    List<int> acquiredItems(List<int> currentState, List<int> recipe)
    {
        var lookup2 = currentState.ToLookup(str => str);

        var result = from str in recipe
                     group str by str into strGroup
                     let missingCount
                          = Math.Max(0, strGroup.Count() - lookup2[strGroup.Key].Count())
                     from missingStr in strGroup.Take(missingCount)
                     select missingStr;
        foreach (int i in result)
        {
            Debug.LogWarning(i);
        }
        return result.ToList();
    }
    public void instanitateTooltip(Repairable r)
    {
        foreach (int id in r.recipe.required_items)
        {
            rtp.gameObject.transform.GetChild(id).gameObject.SetActive(true);
            rtp.gameObject.transform.GetChild(id).gameObject.GetComponent<Image>().sprite = slotEmpty;
        }
        }
    public void populateTooltip(Repairable r)
    {
        r.recipe.required_items.Sort();
        r.currentState.Sort();
        int ii = 0;
        foreach (int id in r.currentState)
        {
            Debug.LogWarning(rtp.gameObject.transform.GetChild(ii).gameObject.name);
            rtp.gameObject.transform.GetChild(ii).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = _id.getItemFromId(id).icon;
            ii = ii + 1;
        }
    }
}
