using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class PoolingSystem : MonoBehaviour
{
    private Queue<ObjectForPool> pool = new Queue<ObjectForPool>();
    private List<ObjectForPool> CreatedObjects = new List<ObjectForPool>();
    [SerializeField] private int StartPoolAmount;
    protected ObjectForPool prototypeForPool;
    private ObjectForPool temporaryObject;

    protected virtual void Awake()
    {
        SetPrototypeForPool();
        CreatPool();
    }
    public void SetToPool(ObjectForPool objectForPool) 
    {
        pool.Enqueue(objectForPool);
    }
    protected abstract void SetPrototypeForPool();
    protected GameObject GetFromPool()
    {          
        if(pool.Count == 0) 
        {
            AddToPool();
        }     
        return pool.Dequeue().gameObject;
    }
    protected void DeactivateAll()
    {
        //pool.Clear();
        foreach (var item in CreatedObjects)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void CreatPool()
    {
        for (int i = 0; i < StartPoolAmount; i++)
        {
            AddToPool();
        }
    }
    private  void AddToPool() 
    {
        temporaryObject = Instantiate(prototypeForPool);
        CreatedObjects.Add(temporaryObject);
        pool.Enqueue(temporaryObject);
    }
}
