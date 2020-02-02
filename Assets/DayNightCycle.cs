using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration;
    Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim["DayNight"].wrapMode = WrapMode.Loop;
    }

    // Update is called once per frame
    void Update()
    {
        if (dayDuration != 0)
        {
            anim["DayNight"].speed = 1.0f / dayDuration;
        }
    }
}
