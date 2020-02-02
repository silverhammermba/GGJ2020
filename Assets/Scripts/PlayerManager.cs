using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float exposureLevel;
    public float maxExposureLevel;
    public float exposureLevelMultiplier;
    public float exposureClearMultiplier;
    public ExposureBarController _exp;
    public bool isHome;
    // Start is called before the first frame update
    void Start()
    {
        exposureLevel = maxExposureLevel;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Home")
        {
            this.isHome = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Home")
        {
            this.isHome = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Home")
        {
            this.isHome = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _exp.SetExposureLevel(exposureLevel / maxExposureLevel);
        if (isHome)
        {
            exposureLevel += Time.deltaTime * exposureClearMultiplier;
            if(exposureLevel > maxExposureLevel)
            {
                exposureLevel = maxExposureLevel;
            }
        }
        else 
        {
            exposureLevel -= Time.deltaTime * exposureLevelMultiplier;
            if(exposureLevel <= 0.0f) 
            {
                // kill the player
            }
        }
    }
}
