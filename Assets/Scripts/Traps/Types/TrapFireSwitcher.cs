using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFireSwitcher : MonoBehaviour
{
    public TrapFire myTrap;
    [SerializeField] private float timeNotActive = 2f;

    //private float countdown;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    countdown -= Time.deltaTime;
    //}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if (countdown > 0)
        //{
        //    return;
        //}

        if (collision.GetComponent<Player>() != null)
        {
            //countdown = timeNotActive;

            myTrap.FireSwitchAfter(timeNotActive);
            animator.SetTrigger("press");
        }
    }
}
