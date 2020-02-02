using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTooltip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.activeSelf) 
        {
            this.gameObject.SetActive(false);
        }
    }
}
