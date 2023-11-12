using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [Header("Bat info")]
    [SerializeField] private Transform[] idlePoints;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform player;
    [SerializeField] private bool agressive;

    private float defaultSpeed;
    private int idleIndex;
    private Vector2 destination;
    private bool playerDeteceted;
    private bool canBeAgressive = true;

    protected override void Start()
    {
        base.Start();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        defaultSpeed = speed;
        destination = idlePoints[0].position;
        transform.position = destination;
    }

    void Update()
    {
        animator.SetBool("canBeAgressive", canBeAgressive);
        animator.SetFloat("speed", speed);
        idleTimeCounter -= Time.deltaTime;

        if (transform.position.x < destination.x && facingDirection == -1)
        {
            Flip();
        }
        else if (transform.position.x > destination.x && facingDirection == 1)
        {
            Flip();
        }

        if (idleTimeCounter > 0) return;

        playerDeteceted = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);

        if (playerDeteceted && !agressive && canBeAgressive)
        {
            canBeAgressive = false;
            agressive = true;
            destination = player.position;
        }

        if (agressive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                idleIndex = UnityEngine.Random.Range(0, idlePoints.Length);

                agressive = false;

                destination = idlePoints[idleIndex].position;

                speed *= .5f;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                if (!canBeAgressive)
                {
                    idleTimeCounter = idleTime;
                }

                canBeAgressive = true;
                speed = defaultSpeed;
            }
        }
    }
}
