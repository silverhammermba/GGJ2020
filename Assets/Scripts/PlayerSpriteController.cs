using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    PlayerController player;
    Animation jumpAnimation;
    private Animator _anim;
    public string jumpAnim, walkAnim, idleAnim, dashAnim;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        jumpAnimation = GetComponent<Animation>();
        _anim = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WalkAnimation() 
    {
        _anim.Play(walkAnim);
    }

    public void IdleAnimation() 
    {
        _anim.Play(idleAnim);
    }
    public void JumpAnimation(float duration)
    {
        _anim.speed = 1.0f / duration;
        _anim.Play(jumpAnim);
    }

    public void JumpDone()
    {
        //jumpAnimation.Stop();
        //jumpAnimation.Rewind();
    }
}
