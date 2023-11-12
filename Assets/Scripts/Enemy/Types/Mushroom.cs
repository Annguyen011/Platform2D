using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{

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
        MoveAround();
    }
}
