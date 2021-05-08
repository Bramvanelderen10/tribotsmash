using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectPooler : MonoBehaviour {

    [Serializable]
    public struct PooledObjectPrefab
    {
        public string Name;
        public int PoolSize;
        public GameObject Prefab;
    }

    public struct PooledObject
    {
        public string Name;
        public int PoolSize;
        public List<GameObject> ObjectPool;

        public PooledObject(string name, int poolSize, List<GameObject> objectPool) : this()
        {
            this.Name = name;
            this.PoolSize = poolSize;
            this.ObjectPool = objectPool;
        }
    }

    public static ObjectPooler Instance;
    public List<PooledObjectPrefab> pooledObjects;

    private List<PooledObject> _objectPools = new List<PooledObject>();

    void Awake()
    {
        Instance = this;
    }

	void Start () {
        if (pooledObjects == null)
            pooledObjects = new List<PooledObjectPrefab>();

        for (var i = 0; i < pooledObjects.Count; i++)
        {
            var list = new List<GameObject>();
            var go = new GameObject {name = pooledObjects[i].Name + "Pool"};

            for (var o = 0; o < pooledObjects[i].PoolSize; o++)
            {
                var obj = Instantiate(pooledObjects[i].Prefab);
                obj.gameObject.SetActive(false);
                obj.gameObject.transform.parent = go.transform;
                list.Add(obj);
            }
            _objectPools.Add(new PooledObject(pooledObjects[i].Name, pooledObjects[i].PoolSize, list));            
        }
	}    

    public GameObject GetPooledObject(string type)
    {
        for (var i = 0; i < _objectPools.Count; i++)
        {
            if (!_objectPools[i].Name.Equals(type)) continue;
            var pool = _objectPools[i].ObjectPool;
            for (var o = 0; o < pool.Count; o++)
            {
                if (!pool[o].gameObject.activeInHierarchy)
                    return pool[o].gameObject;
            }
        }

        return null;
    }
}
