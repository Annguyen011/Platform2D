using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool invisible;

    [Header("Base class")]
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected LayerMask whatisground;
    [SerializeField] protected float groundcheckdistance;
    [SerializeField] protected float wallcheckdistance;
    [SerializeField] protected float idleTime;
    [SerializeField] protected float speed;
    [SerializeField] protected float checkPlayerDistance = 10f;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;


    protected RaycastHit2D playerdetected;
    protected bool canMove = true;
    protected float idleTimeCounter;
    protected bool isGrounded;
    protected bool isWallDetected;
    protected float facingDirection = -1;
    protected Animator animator;
    protected Rigidbody2D rb;



    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null) groundCheck = transform.GetChild(0);
        if (wallCheck == null) wallCheck = transform.GetChild(1);
    }

    protected virtual void Start()
    {
        rb.freezeRotation = true;

    }

    protected virtual void FixedUpdate()
    {
        AnimatorController();
        CollisionCheck();

    }

    protected virtual void MoveAround()
    {
        if (idleTimeCounter <= 0 && canMove)
        {
            rb.velocity = new Vector2(facingDirection * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        idleTimeCounter -= Time.deltaTime;


        if (isWallDetected || !isGrounded)
        {
            Flip();
            idleTimeCounter = idleTime;
        }
    }

    public virtual void Damage()
    {
        canMove = false;
        if (!invisible)
            animator.SetTrigger("gotHit");
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            Player player = collision.collider.GetComponent<Player>();

            player.KnockBack(transform);
        }
    }

    protected virtual void Flip()
    {
        this.facingDirection *= -1;
        this.transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionCheck()
    {
        playerdetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, checkPlayerDistance, playerMask);

        this.isGrounded = Physics2D.Raycast(groundCheck.transform.position, Vector2.down,
            this.groundcheckdistance, this.whatisground);
        this.isWallDetected = Physics2D.Raycast(wallCheck.transform.position, Vector2.right * facingDirection,
            this.wallcheckdistance, whatisground);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(wallCheck.transform.position.x + wallcheckdistance, wallCheck.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, groundCheck.position.y - groundcheckdistance));
    }

    protected virtual void AnimatorController()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

}
