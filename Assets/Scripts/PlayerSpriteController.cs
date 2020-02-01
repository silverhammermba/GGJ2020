using System.Collections;
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

    public void JumpAnimation()
    {
        jumpAnimation.Play();
    }

    public void JumpDone()
    {
        player.JumpDone();
    }
}
