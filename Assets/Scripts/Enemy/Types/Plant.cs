using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Enemy
{
    [SerializeField] private Transform bulletPoint;


    protected override void Start()
    {
        base.Start();

        idleTimeCounter = idleTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        idleTimeCounter -= Time.deltaTime;

        if (idleTimeCounter < 0)
        {
            idleTimeCounter = idleTime;

            animator.SetTrigger("attack");
        }
    }

    public void AttackEvent()
    {
        GameObject newBullet = PoolingObject.instance.Get(0,bulletPoint.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetupSpeed(speed * facingDirection, 0);
    }
}
