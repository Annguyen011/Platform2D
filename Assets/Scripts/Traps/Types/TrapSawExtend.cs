using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSawExtend : Trap
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float distance = .15f;
    [SerializeField] private Transform[] movePoints;
    private int moveIndex;
    private bool goingForward = true;

    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        moveIndex = 0;
        this.animator.SetBool("isWorking", true);
    }

    private void Update()
    {
        MovePoint();
    }

    private void MovePoint()
    {

        if (Vector2.Distance(this.transform.position, this.movePoints[moveIndex].position) < this.distance)
        {
            Flip();
            moveIndex = (goingForward) ? moveIndex + 1 : moveIndex - 1;

            if (moveIndex == 0)
            {
                goingForward = true;
                Flip();
            }
            else
           if (moveIndex > movePoints.Length - 1)
            {
                this.moveIndex = movePoints.Length - 1;
                goingForward = false;
                Flip();
            }
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoints[moveIndex].position, Time.deltaTime * speed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
