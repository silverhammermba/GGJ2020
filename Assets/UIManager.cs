using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Canvas mainUi;
    public RepairTooltip rtp;
    public Sprite slotFilled;
    public Sprite slotEmpty;
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
    public void populateTooltip(Repairable r)
    {
        int i = 0;
        r.recipe.required_items.Sort();
        foreach (int id in r.recipe.required_items)
        {
            rtp.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            rtp.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = slotEmpty;
            if (r.currentState.Count > 0)
            {
                if (r.currentState[i] == r.recipe.required_items[i])
                {
                    Debug.Log("Current State has an object!");
                    rtp.gameObject.transform.GetChild(i).gameObject.SetActive(true);
                    rtp.gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = slotFilled;
                }
            }
        }
        for(int f = 0; f < r.currentState.Count; f++){
            Debug.Log(r.currentState[f]);
        }
    }
}
