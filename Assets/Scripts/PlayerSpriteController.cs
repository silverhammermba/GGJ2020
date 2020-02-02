﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    Animation jumpAnimation;
    SpriteRenderer sprite;
    AudioSource audioSource;
    private Animator _anim;
    public string jumpAnim, walkAnim, idleAnim, dashAnim;
    public List<AudioClip> stepSounds;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        jumpAnimation = GetComponent<Animation>();
        _anim = this.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void flipX(bool flip)
    {
        sprite.flipX = flip;
    }

    public void WalkAnimation()
    {
        _anim.speed = 1.0f;
        _anim.Play(walkAnim);
    }

    public void IdleAnimation()
    {
        _anim.speed = 1.0f;
        _anim.Play(idleAnim);
    }
    public void JumpAnimation(float duration)
    {
        _anim.speed = 1.0f / duration;
        _anim.Play(jumpAnim);
    }

    public void StepSound()
    {
        audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Count)], 0.21f);
    }

    public void JumpSound()
    {
        audioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Count)], 0.42f);
    }
}
