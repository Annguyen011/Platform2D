using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float idleTime;

    private float idleTimeCounter;
    private bool isAggresstive;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (idleTimeCounter <= 0)
        {
            rb.velocity = new Vector2(facingDirection * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        idleTimeCounter -= Time.deltaTime;

        CollisionCheck();

        if (isWallDetected || !isGrounded)
        {
            Flip();
            idleTimeCounter = idleTime;
        }

        AnimatorController();
    }
}
