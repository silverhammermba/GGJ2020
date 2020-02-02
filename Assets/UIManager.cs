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

    public GameObject textBox;
    public Text textValue;

    public bool textLoading = false;
    public string targetString;

    public static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(UIManager).Name;
                    instance = obj.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    public void Start()
    {
        _id = ItemDatabase.Instance;
    }
    public void enabeTooltip(Repairable r)
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
        //nstanitateTooltip(r);
        r.rd.getRecipeFromId(r.recipe_id).required_items.Sort();
        r.currentState.Sort();
        int ii = 0;
        foreach (int id in r.currentState)
        {
            rtp.gameObject.transform.GetChild(ii).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = _id.getItemFromId(id).icon;
            ii = ii + 1;
        }
    }
    public void clearTooltip(Repairable r)  
    {
        //nstanitateTooltip(r);
        int iz = 0;
        foreach (int id in r.recipe.required_items)
        {
            rtp.gameObject.transform.GetChild(iz).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = slotEmpty;
            iz = iz + 1;
        }
    }
    public void setText(string s) 
    {
        textBox.SetActive(true);
        textValue.text = "";
        textLoading = true;
        StopAllCoroutines();
        targetString = s;
        StartCoroutine(displayText(s));
    }
    IEnumerator displayText(string s)
    {
        for (int i = 0; i < s.Length + 1; i++)
        {
            textValue.text = s.Substring(0, i);
            yield return new WaitForSeconds(0.03f);
        }
        textLoading = false;
        yield return new WaitForSeconds(5.0f);
        textBox.SetActive(false);
    }

}
