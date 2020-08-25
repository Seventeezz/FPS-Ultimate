using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    
    private List<GameObject> pooledObjects;
    private GameObject objectToPool;
    private int amountToPool;
    private bool shouldExpand;

    public void Init(GameObject objectToPool, int amountToPool, bool shouldExpand)
    {
        this.objectToPool = objectToPool;
        this.amountToPool = amountToPool;
        this.shouldExpand = shouldExpand;
    }


    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            var go = Instantiate(objectToPool);
            go.SetActive(false);
            pooledObjects.Add(go);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (shouldExpand)
        {
            var go = Instantiate(objectToPool);
            go.SetActive(false);
            pooledObjects.Add(go);
            return go;
        }
        return null;
    }
    
    
}
