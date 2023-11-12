using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [SerializeField] private Transform bulletPoint;

    protected override void Start()
    {
        base.Start();

    }

    private void Update()
    {
        idleTimeCounter -= Time.deltaTime;

        if(idleTimeCounter < 0)
        {
            AttackAgain();

            idleTimeCounter = idleTime;
        }
    }

    public void AttackEvent()
    {
        GameObject newBullet = PoolingObject.instance.Get(1, bulletPoint.position, Quaternion.identity);

        newBullet.GetComponent<Bullet>().SetupSpeed(0, -3f);
    }

    private void AttackAgain()
    {
        animator.SetTrigger("attack");
    }
}
