using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Trap
{
    [SerializeField] private float timeLife = 6f;

    private float xSpeed;
    private float ySpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
    }

    private void OnEnable()
    {
        Invoke(nameof(DisableObject), timeLife);
    }

    private void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void SetupSpeed(float x, float y)
    {
        xSpeed = x;
        ySpeed = y;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
