using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float doubleJumpForce = 10f;
    [SerializeField] private float bufferJumpTime;
    [SerializeField] private float cayoteJumpTime;
    private bool canHaveCayoteJump;
    private float cayoteJumpCounter;
    private bool canMove;
    private bool canDoubleJump;
    private bool facingRight = true;
    private int facingDirection = 1;
    private float bufferJumpCounter;
    private float defaultJumpForce;

    [Header("Slide info")]
    private bool canWallSlide;
    private bool isWallSliding;

    [Header("Knockback info")]
    [SerializeField] private Vector2 knockBackDirection;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackProtectionTime;
    private bool isKnockback;
    private bool canBeKnockback = true;

    [Header("Collision info")]
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private float wallCheckDistance = .7f;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    private bool isWallDetected;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void Start()
    {
        this.rb.freezeRotation = true;
        this.rb.gravityScale = 4f;

        defaultJumpForce = jumpForce;
    }

    private void Update()
    {

        bufferJumpTime -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        this.AnimationController();

        if (isKnockback) return;

        this.CollisionCheck();
        this.Move();
        this.InputCheck();

        this.FlipController();


        WallSlide();
        CheckForEnemy();

    }

    private void CheckForEnemy()
    {
        Collider2D[] hitedColl = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        foreach (Collider2D coll in hitedColl)
        {
            if (coll.GetComponent<Enemy>() != null)
            {
                if (coll.GetComponent<Enemy>().invisible) return;

                if (coll.GetComponent<Enemy>().transform.position.y < transform.position.y)
                {
                    coll.GetComponent<Enemy>().Damage();

                    Jump();
                }
            }
        }
    }

    public void KnockBack(Transform DamageTransform)
    {
        if (!canBeKnockback) return;
        isKnockback = true;
        canBeKnockback = false;

        float direction;
        direction = (transform.position.x > DamageTransform.position.x) ? 1 : -1;

        rb.velocity = new Vector2(knockBackDirection.x * direction, knockBackDirection.y * direction);

        Invoke(nameof(CancelKnockback), knockbackTime);
        Invoke(nameof(AllowKnockback), knockbackProtectionTime);
    }

    private void AllowKnockback()
    {
        canBeKnockback = true;
    }

    private void CancelKnockback()
    {
        isKnockback = false;
    }

    private void WallSlide()
    {
        if (this.isWallDetected && this.rb.velocity.y < 0f)
        {
            this.canWallSlide = true;
        }
        if (!this.isWallDetected)
        {
            this.canWallSlide = false;
            this.isWallSliding = false;
        }

        if (this.canWallSlide)
        {
            this.isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .1f);
        }
    }

    private void AnimationController()
    {
        this.animator.SetBool("isMoving", Input.GetAxisRaw("Horizontal") != 0f);
        this.animator.SetBool("isGrounded", this.isGrounded);
        this.animator.SetBool("isWallSliding", this.isWallSliding);
        this.animator.SetBool("isWallDetected", this.isWallDetected);
        this.animator.SetBool("isKnocked", this.isKnockback);
        this.animator.SetFloat("yVelocity", this.rb.velocity.y);
    }

    private void WallJump()
    {
        rb.velocity = new Vector2(-facingDirection * 5f, jumpForce);
    }

    private void InputCheck()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            this.canWallSlide = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            this.JumpButton();
        }
    }

    private void JumpButton()
    {
        if (!isGrounded)
        {
            bufferJumpCounter = bufferJumpTime;
        }

        if (this.isWallSliding)
        {
            this.WallJump();
            this.canDoubleJump = true;
        }

        else if (this.isGrounded || cayoteJumpCounter > 0)
        {
            this.Jump();


        }
        else if (this.canDoubleJump)
        {
            this.canDoubleJump = false;
            jumpForce = doubleJumpForce;
            this.Jump();
            jumpForce = defaultJumpForce;
        }

        this.canWallSlide = false;
    }

    private void Move()
    {
        if (this.isGrounded)
        {
            this.canDoubleJump = true;
            canHaveCayoteJump = true;

            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter -= 1;

                Jump();
            }
        }
        else
        {
            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }

        }


        this.canMove = !this.isWallSliding;
        if (!this.canMove && this.isGrounded)
        {
            this.canMove = true;
        }
        if (!this.canMove) return;
        this.rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") *
            this.moveSpeed, this.rb.velocity.y);
    }

    private void FlipController()
    {
        if (this.facingRight && this.rb.velocity.x < 0f)
        {
            this.Flip();
        }
        else if (!this.facingRight && this.rb.velocity.x > 0f)
        {
            this.Flip();
        }
    }

    private void Flip()
    {
        this.facingDirection *= -1;
        this.facingRight = !facingRight;
        this.transform.Rotate(0, 180, 0);
    }

    private void CollisionCheck()
    {
        this.isGrounded = Physics2D.Raycast(transform.position, Vector2.down,
            this.groundCheckDistance, this.whatisground);
        this.isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection,
            this.wallCheckDistance, whatisground);
    }

    private void Jump()
    {
        this.rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }
}
