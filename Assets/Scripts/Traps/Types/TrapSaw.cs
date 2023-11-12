using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : Trap
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float coolDown = 1f;
    [SerializeField] private float distance = .15f;
    [SerializeField] private Transform[] movePoints;
    private bool isWorking;
    private float coolDownTimer;
    private int moveIndex;

    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        moveIndex = 0;
    }

    private void Update()
    {
        AnimatorController();
        MovePoint();
    }

    private void MovePoint()
    {
        coolDownTimer -= Time.deltaTime;
        this.isWorking = coolDownTimer <= 0;

        if (Vector2.Distance(this.transform.position, this.movePoints[moveIndex].position) < this.distance)
        {
            Flip();

            this.isWorking = false;

            coolDownTimer = coolDown;
            moveIndex++;

            if (moveIndex > movePoints.Length - 1)
            {
                this.moveIndex = 0;
            }
        }

        if (this.isWorking)
            this.transform.position = Vector3.MoveTowards(this.transform.position, movePoints[moveIndex].position, Time.deltaTime * speed);
    }

    private void AnimatorController()
    {
        this.animator.SetBool("isWorking", isWorking);
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
