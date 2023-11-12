using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField] private float activeTime;
    [SerializeField] private Transform player;

    private SpriteRenderer spriteRenderer;
    private float activeTimeCounter;
    private bool agressive = true;
    private int ranInt;

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {

        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if (activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && agressive)
        {
            ChosePosition();
            agressive = false;
            idleTimeCounter = idleTime;
            animator.SetTrigger("disappear");
        }

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && !agressive)
        {
            agressive = true;
            activeTimeCounter = activeTime;
            animator.SetTrigger("appear");
        }

        if (transform.position.x < player.position.x && facingDirection == -1)
        {
            Flip();
        }
        else if (transform.position.x > player.position.x && facingDirection == 1)
        {
            Flip();
        }


    }

    private void ChosePosition()
    {
        ranInt = UnityEngine.Random.Range(0, 5);
        switch (ranInt)
        {
            case 0: ranInt = 4; break;
            case 1: ranInt = -4; break;
            case 2: ranInt = 6; break;
            case 3: ranInt = -6; break;
            case 4: ranInt = 5; break;

        }
        transform.position = new Vector2(player.position.x + ranInt, player.position.y + ranInt);
    }

    public void Appear()
    {
        spriteRenderer.enabled = true;
    }

    public void Disapear()
    {
        spriteRenderer.enabled = false;
    }

    public override void Damage()
    {
        if (agressive)
        {
            base.Damage();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (agressive)
        {
            base.OnCollisionEnter2D(collision);
        }

    }
}
