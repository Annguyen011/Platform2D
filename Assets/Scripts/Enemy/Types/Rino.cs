using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : Enemy
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float idleTime;
    [SerializeField] private float shockTime;
    [SerializeField] private LayerMask playerMask;

    private bool playerdetected;
    private bool isAggresstive;
    private float idleTimeCounter;
    private float shockTimeCounter;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); 
        invisible = true;
    }

    private void Update()
    {
        idleTimeCounter -= Time.deltaTime;
        shockTimeCounter -= Time.deltaTime;

        playerdetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, 4f, playerMask);

        if (playerdetected)
        {
            isAggresstive = true;
        }

        if (!isAggresstive)
        {
            if (idleTimeCounter <= 0)
                rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
            else
                rb.velocity = Vector2.zero;

            if (!isGrounded || isWallDetected)
            {
                Flip();

                idleTimeCounter = idleTime;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed * 1.5f * facingDirection, rb.velocity.y);

            if (isWallDetected && invisible)
            {
                invisible = false;
                print("Run");
                shockTimeCounter = shockTime;
                rb.velocity = Vector2.zero;
            }
        }

        if (shockTimeCounter < 0 && !invisible)
        {
            invisible = true;
            Flip();
            isAggresstive = false;
        }
        animator.SetBool("invisible", invisible);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
