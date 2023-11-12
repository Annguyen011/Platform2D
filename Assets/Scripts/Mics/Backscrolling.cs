using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backscrolling : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed = 1f;

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void FixedUpdate()
    {
        material.mainTextureOffset += direction * speed * Time.fixedDeltaTime;
    }
}
