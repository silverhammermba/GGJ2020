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
    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        exposureLevel = maxExposureLevel;
        _gm = GameManager.Instance;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Home")
        {
            this.isHome = true;
        }
        if(col.tag == "TeleportArea")
        {
            TeleportArea t = col.GetComponent<TeleportArea>();
            if(t) 
            {
                _gm.TeleportPlayerToLocation(t.teleportLocation);
            }
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
