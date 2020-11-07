using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectForPool : MonoBehaviour
{
    [SerializeField] private PoolingSystem poolingSystem;

    protected virtual void OnDisable()
    {
        poolingSystem.SetToPool(this);
    }
}
