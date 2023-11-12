using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : Singleton<PoolingObject>
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private List<GameObject>[] pools;

    protected override void Awake()
    {
        base.Awake();

        pools = new List<GameObject>[prefabs.Length];
       
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, Vector3 spawnPosition, Quaternion rotation)
    {
        GameObject select = null;

        foreach(GameObject prefab in pools[index])
        {
            if (!prefab.activeSelf)
            {
                select = prefab;
                select.SetActive(true);

                break;
            }
        }

        if(!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        select.transform.SetPositionAndRotation(spawnPosition, rotation);

        return select;
    }
}
