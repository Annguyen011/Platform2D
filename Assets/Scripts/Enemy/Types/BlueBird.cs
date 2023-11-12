using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : Enemy
{
    [SerializeField] private float groundAbovedetectedDistance;

    private RaycastHit2D groundAbovedetected;

    [SerializeField] private Vector2 flyDirection;
    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;
    private float flyForce;
    private bool canFly = true;

    protected override void Start()
    {
        base.Start();

        rb.gravityScale = 0f;
    }

    private void Update()
    {
        if (groundAbovedetected)
        {
            flyForce = flyDownForce;
        }else if (isGrounded)
        {
            flyForce = flyUpForce; 
        }

        if (isWallDetected)
        {
            Flip();
        }
    }

    public override void Damage()
    {
        base.Damage();

        canFly = false;
    }

    public void FlyEvent()
    {
        if (!canFly) return;
        rb.velocity = new Vector2(flyDirection.x * speed * facingDirection, flyForce);
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();

        groundAbovedetected = Physics2D.Raycast(transform.position, Vector2.up, groundAbovedetectedDistance, whatisground);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, groundCheck.position.y + groundAbovedetectedDistance));
    }
}
