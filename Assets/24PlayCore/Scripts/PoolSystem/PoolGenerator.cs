using System;
using System.Collections.Generic;
using UnityEngine;
using TFPlay;

public class PoolGenerator : MonoBehaviour
{
    [Serializable]
    public class PoolData
    {
        public string name;
        public PoolObject prefab;
        public int count;
    }

    [SerializeField]
    private List<PoolData> pools;

    private PoolObject[] poolObjects;

    private void OnValidate()
    {
        if (pools != null)
        {
            foreach (var p in pools)
            {
                p.name = p.prefab != null ? p.prefab.name : string.Empty;
            }
        }
    }

    private void Start()
    {
        CreatePools();
        GameC.Instance.OnLevelStartLoading += OnLevelStartLoading;
    }

    private void OnLevelStartLoading(int level)
    {
        var poolObjectsCount = poolObjects.Length;
        for (int i = 0; i < poolObjectsCount; i++)
        {
            poolObjects[i].Destroy();
        }
    }

    private void CreatePools()
    {
        foreach (var p in pools)
        {
            PoolManager.Instance.CreatePool(p.prefab.gameObject, p.count);
        }

        poolObjects = GetComponentsInChildren<PoolObject>(true);
    }
}
