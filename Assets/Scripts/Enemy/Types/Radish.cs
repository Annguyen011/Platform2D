using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Radish : Enemy
{
    [SerializeField] private float groundBelowdetectedDistance;
    [SerializeField] private float groundAbovedetectedDistance;
    [SerializeField] private float agroTime;

    private float agroTimeCounter;
    private bool agressive;
    private RaycastHit2D groundBelowdetected;
    private bool groundAbovedetected;


    private void Update()
    {
        agroTimeCounter += Time.deltaTime;
        if (agroTimeCounter > agroTime && !groundBelowdetected)
        {
            agressive = false;
            agroTimeCounter = 0;
            rb.gravityScale = 1f;
        }

        if (!agressive)
        {
            if (groundBelowdetected && !groundAbovedetected)
            {
                rb.velocity = Vector2.up;
            }
        }
        else
        {
            if (groundBelowdetected.distance <= 1.25f)
                MoveAround();
        }
    }

    public override void Damage()
    {
        if (!agressive)
        {
            agressive = true;
            rb.gravityScale = 12f;
        }

        else if (agressive)
        {
            base.Damage();
        }
    }

    protected override void AnimatorController()
    {
        base.AnimatorController();

        animator.SetBool("agressive", agressive);
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();

        groundAbovedetected = Physics2D.Raycast(transform.position, Vector2.up, groundAbovedetectedDistance, whatisground);
        groundBelowdetected = Physics2D.Raycast(transform.position, Vector2.down, groundBelowdetectedDistance, whatisground);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, groundCheck.position.y + groundAbovedetectedDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, groundCheck.position.y - groundBelowdetectedDistance));
    }
}
