﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    PlayerController player;
    Animation jumpAnimation;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        jumpAnimation = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpAnimation(float duration)
    {
        jumpAnimation["Jumping"].speed = 1.0f / duration;
        jumpAnimation.Play();
    }

    public void JumpDone()
    {
        jumpAnimation.Stop();
        jumpAnimation.Rewind();
    }
}
