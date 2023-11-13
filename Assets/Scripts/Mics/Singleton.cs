using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Has a instance: " + transform.name);
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this as T;
        }
    }
}
