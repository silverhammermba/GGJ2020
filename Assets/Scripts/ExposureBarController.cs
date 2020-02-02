using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExposureBarController : MonoBehaviour
{
    private Image _im;
    public Sprite fullExp, mediumExposure, lowExposure;
    public float medExpThres, lowExpThresh;
    // Start is called before the first frame update
    void Start()
    {
        _im = this.GetComponent<Image>();
    }

    public void SetExposureLevel(float expLevel)
    {
        _im.fillAmount = expLevel;
        if(expLevel > medExpThres)
        {
            _im.sprite = fullExp;
        }
        else if(expLevel < medExpThres && expLevel > lowExpThresh)
        {
            _im.sprite = mediumExposure;
        }
        else
        {
            _im.sprite = lowExposure;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
