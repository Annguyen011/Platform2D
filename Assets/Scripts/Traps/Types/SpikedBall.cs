using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : Trap
{
    [SerializeField] private Vector2 pushDirection;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
