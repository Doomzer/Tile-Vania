using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 15f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    bool isClimbing = false;
    float normalGravity;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        normalGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            bool moving = Run();
            Jump();
            FlipSprite(moving);
            Climb();
            playerDied();
        }
    }

    void playerDied()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().PlayerDeath();
            isAlive = false;
        }
    }

    bool Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon && !isClimbing;
        myAnimator.SetBool("Running", playerMoving);
        return playerMoving;
    }

    void Climb()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = 0f;
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
            myRigidbody.velocity = playerVelocity;
            bool playerMoving = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("Climbing", playerMoving);
            isClimbing = playerMoving;
        }
        else
        {
            myRigidbody.gravityScale = normalGravity;
            myAnimator.SetBool("Climbing", false);
            isClimbing = false;
        }
    }

    void Jump()
    {
        if (myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {

                Vector2 jumpVelocity = new Vector2(0f, jumpHeight);
                myRigidbody.velocity += jumpVelocity;

            }
        }
    }

    void FlipSprite(bool playerMoving)
    {
        if(playerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}
