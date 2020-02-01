﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpDistance;
    public float jumpSpeed;
    public float jumpHeight;

    Rigidbody2D rigidBody;
    CircleCollider2D playerCollider;
    CircleCollider2D jumpTester;
    Vector2 move;
    bool startedTestingJump;
    bool jumpFailed;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponents<CircleCollider2D>()[0];
        jumpTester = GetComponents<CircleCollider2D>()[1];
    }

    void Start()
    {
        jumpTester.radius = playerCollider.radius;
        startedTestingJump = false;
        jumpFailed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        move = gamepad.leftStick.ReadValue() * speed;

        if (startedTestingJump)
        {
            startedTestingJump = false;
            if (jumpFailed)
            {
                // TODO: indicate jump failed somehow????
                Debug.Log("Jump failed");
            }
            else
            {
                // TODO: do jump
                Debug.Log("Jump succeeded");
            }
        }
        else if (gamepad.buttonSouth.isPressed)
        {
            StartJump(move);
        }

        rigidBody.AddForce(move);
    }

    void StartJump(Vector2 direction)
    {
        if (startedTestingJump) return;

        startedTestingJump = true;
        jumpFailed = false;
        jumpTester.offset = move.normalized * jumpDistance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (startedTestingJump)
        {
            jumpFailed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (startedTestingJump)
        {
            jumpFailed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }
}
