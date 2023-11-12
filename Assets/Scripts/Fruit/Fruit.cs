using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitType type = FruitType.Apple;

    [SerializeField] private RuntimeAnimatorController[] animController;

    private Animator animator;
    private bool isRandom;
    private int randomInt;
    private int randomOpportunity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        randomOpportunity = UnityEngine.Random.Range(0, 2);

        isRandom = randomOpportunity == 0;

        randomInt = UnityEngine.Random.Range(0, (int)Enum.GetNames(typeof(FruitType)).Length);
        animator.runtimeAnimatorController = (!isRandom) ? animController[(int)type] : animController[randomInt];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().fruits++;
            gameObject.SetActive(false);
        }
    }
}

public enum FruitType
{
    Apple,
    Banana,
    Cherri,
    Kiwi,
    Melon,
    Orange,
    Pineapple,
    Strawberry
}
