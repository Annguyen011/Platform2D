using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : Trap
{
    public bool isWorking = true;

    private Animator animator;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isWorking = true;

        if (transform.parent == null)
            InvokeRepeating(nameof(FireSwitch), 1f, 3f);
    }

    private void Update()
    {
        this.animator.SetBool("isWorking", isWorking);
    }

    public void FireSwitchAfter(float second)
    {
        CancelInvoke();
        isWorking = false;
        //Invoke("FireSwitch", second);
    }

    public void FireSwitch()
    {
        this.isWorking = !this.isWorking;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isWorking)
            base.OnTriggerEnter2D(collision);
    }
}
