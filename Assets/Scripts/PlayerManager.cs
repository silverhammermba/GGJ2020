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
	private UIManager _um;
    // Start is called before the first frame update
    void Start()
    {
        exposureLevel = maxExposureLevel;
        _gm = GameManager.Instance;
		_um = UIManager.Instance;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Home")
        {
            this.isHome = true;
        }
        if(col.tag == "TeleportArea")
        {
			if(!_gm.isDead)
			{
				TeleportArea t = col.GetComponent<TeleportArea>();
				if (t)
				{
					_gm.TeleportPlayerToLocation(t.teleportLocation);
				}
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
	private IEnumerator deathRoutine()
	{
		_um.FadeToBlack();
		yield return new WaitForSeconds(2.0f);
		_um.showDeathMenu();
		_um.setDeathText("You feel your respirator failing. Everything fades to black..." + "\n\n" + "Press Q to return to the main menu.");
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
				if (!_gm.isDead)
				{
					_gm.isDead = true;
					StartCoroutine(deathRoutine());
				}
            }
        }
    }
}
