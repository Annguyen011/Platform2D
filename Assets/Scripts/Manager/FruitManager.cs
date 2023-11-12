using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();

    private void Reset()
    {
        foreach (Transform item in transform)
        {
            points.Add(item);
        }
    }

    private void Start()
    {
        foreach (Transform item in points)
        {
            PoolingObject.instance.Get(3, item.position, Quaternion.identity).GetComponent<Fruit>();

        }
    }

}
