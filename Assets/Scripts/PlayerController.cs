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
    float nextForce;
    bool startedTestingJump;
    bool jumpFailed;
    bool jumping;
    bool interacting;
    float jumpStartTime;
    Vector3 jumpStartPosition;
    Vector3 jumpEndPosition;
    float dashStartTime;
    GameObject currentlyInteractingObject;
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
        radius = 1;
        jumpTester.radius = playerCollider.radius;
        startedTestingJump = false;
        jumpFailed = false;
        jumping = false;
        dashStartTime = Time.time - dashCooldown;
        nextForce = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!um.isToolTipEnabled())
        {
            canMove = true;
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
        }
       

        rigidBody.AddForce(move * nextForce);
        nextForce = moveForce;
        if (rigidBody.velocity.magnitude > 0.3f)
        {
            if(rigidBody.velocity.x < 0.0f)
            {
                sprite.gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else if(rigidBody.velocity.x > 0.3f)
            {
                sprite.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            sprite.WalkAnimation();
        }
        else 
        {
            sprite.IdleAnimation();
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

    public GameObject attemptInteract()
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
                return hit.gameObject;
            }
        }
        return this.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (startedTestingJump)
        {
            jumpFailed = true;
        }
    }

    private void OnMove(InputValue input)
    {
        move = input.Get<Vector2>().normalized;
    }

    private void OnJump()
    {
        if (um.isToolTipEnabled() && currentlyInteractingObject.GetComponent<Repairable>().isDone())
        {
            // TODO: do the effect of the repair
            um.disableToolTip();
            return;
        }

        if (!canJump || jumping || startedTestingJump || interacting) return;

        TestJump(move);
    }

    private void OnDash()
    {
        if (um.isToolTipEnabled())
        {
            um.disableToolTip();
            return;
        }

        if (!canDash || jumping || startedTestingJump || interacting || dashStartTime + dashCooldown > Time.time) return;

        dashStartTime = Time.time;
        nextForce = dashForce;
    }

    private void OnInteract()
    {
        if (jumping || startedTestingJump || interacting) return;

        canMove = false;
        currentlyInteractingObject = attemptInteract();
        interacting = false;
    }

    private void OnUse()
    {
        if (!um.isToolTipEnabled()) return;

        for (int a = 0; a < gameObject.GetComponent<Inventory>().inventory.Count; a++)
        {
            if (currentlyInteractingObject.GetComponent<Repairable>().repairObject(gameObject.GetComponent<Inventory>().inventory[a]))
            {
                Debug.LogWarning(gameObject.GetComponent<Inventory>().inventory[a].id.ToString() + " was a required item, and will be removed from your inventory");
                gameObject.GetComponent<Inventory>().removeItem(a);
                return;
            }
        }
        um.populateTooltip(currentlyInteractingObject.GetComponent<Repairable>());
    }
}
