using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public struct ObjectPoolInfo
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}


public class ObjectPoolManager : MonoBehaviour
{
    #region Singleton
    
    public static ObjectPoolManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("单例模式错误，存在多个ObjectPoolManager实例！");
            return;
        }
        instance = this;
    }
    
    #endregion
    
    [SerializeField]
    private ObjectPoolInfo[] objectPoolInfos = null;
    
    private Dictionary<string, ObjectPooler> pools;

    private void Start()
    {
        pools = new Dictionary<string, ObjectPooler>();
        foreach (var poolInfo in objectPoolInfos)
        {
            ObjectPooler objectPooler = gameObject.AddComponent<ObjectPooler>();
            objectPooler.Init(poolInfo.objectToPool, poolInfo.amountToPool, poolInfo.shouldExpand);
            pools.Add(poolInfo.objectToPool.name, objectPooler);
        }
    }

    public GameObject GetPooledObject(string prefabName)
    {
        return pools[prefabName].GetPooledObject();
    }
    
    
    
}
