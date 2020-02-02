﻿using System.Collections;
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
    public Vector2 triggerPos;
    public bool isReading;
    public Text textValue;

    public bool textLoading = false;
    public string targetString;

    public Text repairText;

    public RectTransform lootBarTransform;
    public Image lootBarFill;
    private Camera _main;
    public Animator fadeAnimator;

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
        this.triggerPos = this.transform.position;
        _id = ItemDatabase.Instance;
        _main = Camera.main;
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
            rtp.gameObject.transform.GetChild(ii).gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            ii = ii + 1;
        }
        rtp.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = r.rd.getRecipeFromId(r.recipe_id).name;
        rtp.gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = r.rd.getRecipeFromId(r.recipe_id).richText_description;
        if (r.isFinished){
            this.repairText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            rtp.gameObject.transform.GetChild(7).gameObject.SetActive(true);
        }
        else {
            rtp.gameObject.transform.GetChild(7).gameObject.SetActive(false);
            this.repairText.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    public static Vector2 GetAnchoredPositionFromWorldPosition(Vector3 _worldPostion, Camera _camera,  Canvas _canvas)
    {
            //Vector2 myPositionOnScreen = _camera.WorldToScreenPoint(_worldPostion); // for transform.position?
            Vector2 myPositionOnScreen = _camera.WorldToViewportPoint(_worldPostion); //for RectTransform.AnchoredPosition?
            float scaleFactor = _canvas.scaleFactor;
             return new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
    }

    public void UpdateLootBar(Transform t, float f) 
    {
        if(!lootBarTransform.gameObject.activeSelf) 
        {
            lootBarTransform.gameObject.SetActive(true);
        }

        lootBarFill.fillAmount = f;
    }

    public void CloseLootBar() 
    {
        lootBarTransform.gameObject.SetActive(false);
    }
    public void clearTooltip(Repairable r)  
    {
        //nstanitateTooltip(r);
        int iz = 0;
        foreach (int id in r.recipe.required_items)
        {
            rtp.gameObject.transform.GetChild(iz).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = _id.getItemFromId(id).icon;
            rtp.gameObject.transform.GetChild(iz).gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            iz = iz + 1;
        }
        if (r.recipe.required_items.Count < 3)
        {
            int dif = 3 - r.recipe.required_items.Count;
            for (int j = 2; j > dif; j--)
            {
                rtp.gameObject.transform.GetChild(j).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = slotEmpty;
                rtp.gameObject.transform.GetChild(j).gameObject.transform.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f); 
            }
        }
    }
    public void EnableRepairText() 
    {
        this.repairText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    public void DisableRepairText() 
    {
        this.repairText.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
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
        this.isReading = false;
    }

    public void FadeToBlack() 
    {
        fadeAnimator.Play("FadeToBlack");
    }

    public void FadeFromBlack() 
    {
        fadeAnimator.Play("FadeFromBlack");
    }

    private IEnumerator fadeLoc() 
    {
        fadeAnimator.Play("FadeToBlack");
        yield return new WaitForSeconds(2.0f);
        fadeAnimator.Play("FadeFromBlack");
    }

}
