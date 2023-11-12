using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool invisible;

    [Header("Base class")]
    [SerializeField] protected LayerMask whatisground;
    [SerializeField] protected float groundcheckdistance;
    [SerializeField] protected float wallcheckdistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    protected bool isGrounded;
    protected bool isWallDetected;
    protected float facingDirection = -1;
    protected Animator animator;
    protected Rigidbody2D rb;


    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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


    public void Damage()
    {
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
        this.isGrounded = Physics2D.Raycast(groundCheck.transform.position, Vector2.down,
            this.groundcheckdistance, this.whatisground);
        this.isWallDetected = Physics2D.Raycast(wallCheck.transform.position, Vector2.right * facingDirection,
            this.wallcheckdistance, whatisground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(wallCheck.transform.position.x + wallcheckdistance, wallCheck.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(groundCheck.transform.position.x, groundCheck.position.y - groundcheckdistance));
    }

    protected virtual void AnimatorController()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

}
