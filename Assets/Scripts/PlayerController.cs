using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveForce;
    public bool canMove;
    public float jumpDistance;
    public float jumpDuration; // time in seconds to complete the jump
    public bool canJump;
    public float dashForce;
    public float dashCooldown;
    public bool canDash;
    public int radius;
    public UIManager um;
    Rigidbody2D rigidBody;
    CircleCollider2D playerCollider;
    CircleCollider2D jumpTester;
    PlayerSpriteController sprite;

    Vector2 move;
    bool startedTestingJump;
    bool jumpFailed;
    bool jumping;
    bool interacting;
    float jumpStartTime;
    Vector3 jumpStartPosition;
    Vector3 jumpEndPosition;
    float dashStartTime;
    Gamepad gamepad;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponents<CircleCollider2D>()[0];
        jumpTester = GetComponents<CircleCollider2D>()[1];
        sprite = GetComponentInChildren<PlayerSpriteController>();
    }

    void Start()
    {
        canMove = true;
        radius = 5;
        gamepad = Gamepad.current;
        jumpTester.radius = playerCollider.radius;
        startedTestingJump = false;
        jumpFailed = false;
        jumping = false;
        dashStartTime = Time.time - dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!interacting)
        {
            if (!jumping && gamepad.buttonWest.wasPressedThisFrame && !interacting)
            {
                canMove = false;
                attemptInteract();
                interacting = false;
            }
            if (!um.isToolTipEnabled())
            {
                canMove = true;
            }
        }
        if (jumping)
        {
            float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;

            if (jumpProgress >= 1.0f)
            {
                EndJump();
            }
            else
            {
                transform.position = Vector3.Lerp(jumpStartPosition, jumpEndPosition, jumpProgress);
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        move = gamepad.leftStick.ReadValue() * moveForce;

        // mid-jump we don't use physics
        if (jumping) return;

        // if it's the frame after we tested jump, we should know the result
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
                StartJump();

            }
        } // otherwise maybe we can start testing a new jump
        else if (canJump && gamepad.buttonSouth.isPressed)
        {
            TestJump(move);
        }
       

        if (!startedTestingJump && canDash && gamepad.buttonEast.isPressed && dashStartTime + dashCooldown < Time.time)
        {
            dashStartTime = Time.time;
            rigidBody.AddForce(move * (dashForce / moveForce));
        }
        else
        {
            rigidBody.AddForce(move);
        }
    }

    void StartJump()
    {
        jumping = true;
        rigidBody.simulated = false;
        jumpStartPosition = transform.position;
        jumpEndPosition = transform.position + (Vector3)jumpTester.offset;
        jumpStartTime = Time.time;

        sprite.JumpAnimation(jumpDuration);
    }

    void EndJump()
    {
        sprite.JumpDone();
        transform.position = jumpEndPosition;
        rigidBody.simulated = true;
        jumping = false;
    }

    void TestJump(Vector2 direction)
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
    public void attemptInteract()
    {
        interacting = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.gameObject.name);
            if (hit.gameObject.CompareTag("Interactable"))
            {
                Debug.Log("Interacted with an interactable");
                hit.gameObject.GetComponent<Interactable>().interact();

            }
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
