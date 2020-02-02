using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{
    public float moveForce;
    public float timescale = 1f;
    public bool canMove;
    public float jumpDistance;
    public float jumpDuration; // time in seconds to complete the jump
    public bool canJump;
    public float dashForce;
    public float dashCooldown;
    public bool canDash;
    public int radius;
    public Vector2 triggerLoc;
    public UIManager um;
    Rigidbody2D rigidBody;
    CircleCollider2D playerCollider;
    CircleCollider2D jumpTester;
    PlayerSpriteController sprite;
    public bool isPaused;
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
    bool isContinouslyInteracting;
    Describable describable;

    public float interactTime;
    public float maxInteractTime;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponents<CircleCollider2D>()[0];
        jumpTester = GetComponents<CircleCollider2D>()[1];
        sprite = GetComponentInChildren<PlayerSpriteController>();
    }

    void Start()
    {
        timescale = 1f;
        isPaused = false;
        um = UIManager.Instance;
        canMove = true;
        canDash = false;
        canJump = false;
        radius = 1;
        jumpTester.radius = playerCollider.radius;
        startedTestingJump = false;
        jumpFailed = false;
        jumping = false;
        dashStartTime = Time.time - dashCooldown;
        nextForce = 0.0f;
        interactTime = maxInteractTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!um.isToolTipEnabled())
        {
            canMove = true;
        }
        Debug.Log("iscont: " + isContinouslyInteracting.ToString());
        Debug.Log("isInt: " + interacting);
        if (isContinouslyInteracting) 
        {
            canMove = false;
            interactTime -= Time.deltaTime;
            um.UpdateLootBar(this.currentlyInteractingObject.transform, interactTime / maxInteractTime);
            if(interactTime <= 0.0f) 
            {
                this.currentlyInteractingObject.GetComponent<Interactable>().interact();
                this.isContinouslyInteracting = false;
                this.interactTime = this.maxInteractTime;
                this.interacting = false;
                um.CloseLootBar();
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
        attemptRead();
        disableTextboxIfFar();
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

        if (rigidBody.velocity.x > 0.0f) sprite.flipX(false);
        if (rigidBody.velocity.x < 0.0f) sprite.flipX(true);

        rigidBody.AddForce(move * nextForce);
        nextForce = moveForce;
        if (rigidBody.velocity.magnitude > 0.3f)
        {
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

    public Interactable getInteractable(bool isContinous)
    {
        interacting = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.gameObject.name);
            if (hit.gameObject.CompareTag("Interactable"))
            {
                return hit.gameObject.GetComponent<Interactable>();
            }
        }
        interacting = false;
        return null;
    }
    public void disableTextboxIfFar()
    {
        Vector2 currentPos = this.gameObject.transform.position;
        if (Vector2.Distance(currentPos, triggerLoc) > 3.5)
        {
            um.textBox.SetActive(false);
            um.isReading = false;
            describable = null;
        }
    }
    public void attemptRead()
    {
        if (um.isReading) return;
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Describable"))
            {
                Describable newDescribable = hit.gameObject.GetComponent<Describable>();
                if (newDescribable != describable)
                {
                    describable = newDescribable;
                    um.isReading = true;
                    triggerLoc = hit.gameObject.transform.position;
                    um.setText(describable.StartDescription());
                }
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

    private void OnMove(InputValue input)
    {
        move = input.Get<Vector2>().normalized;
    }
    private void OnStopGame() {
        if (Time.timeScale == 0)
        {
            um.unPauseFade();
            Time.timeScale = 1;
        }
        else
        {
            um.pauseFade();
            Time.timeScale = 0;
        }
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

        if(isContinouslyInteracting)
        {
            this.isContinouslyInteracting = false;
            this.currentlyInteractingObject = null;
            this.interactTime = this.maxInteractTime;
            canMove = true;
            interacting = false;
            um.CloseLootBar();
        }

        if (!canDash || jumping || startedTestingJump || interacting || dashStartTime + dashCooldown > Time.time) return;

        dashStartTime = Time.time;
        nextForce = dashForce;
    }

    private void OnInteract()
    {
        if (jumping || startedTestingJump || interacting) return;

        canMove = false;
        Interactable interactableObject = this.getInteractable(false);
        if(interactableObject)
        {
            currentlyInteractingObject = interactableObject.gameObject;
            if(interactableObject.isContinous) 
            {
                // start interacting with continous object
                isContinouslyInteracting = true;
            }
            else 
            {
                currentlyInteractingObject.GetComponent<Interactable>().interact();
                interacting = false;
            }
        }

        if (!isContinouslyInteracting && um.isReading && describable != null)
        {
            string nextText = describable.NextText();
            if (nextText == null)
            {
                um.textBox.SetActive(false);
                um.isReading = false;
            }
            else
            {
                um.setText(nextText);
            }
        }
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
        Debug.Log(currentlyInteractingObject.GetComponent<Repairable>());
        um.populateTooltip(currentlyInteractingObject.GetComponent<Repairable>());
    }
}
