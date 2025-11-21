using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;  
    [Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    private Dictionary<string , Queue<GameObject>> poolDictionary;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poolDictionary = new Dictionary<string , Queue<GameObject>>();    

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform); // Set the parent to the PoolManager for better organization in the hierarchy
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < objectPool.Length; i++)
        //{
        //    if (objectPool[i] == null)
        //    {
        //        // If the object is null, instantiate a new one
        //        objectPool[i] = Instantiate(prefabToPool);
        //        objectPool[i].SetActive(false); // Set it inactive initially
        //    }
        //}
    }

    public Dictionary<string, Queue<GameObject>> getPoolDictionary() {
        return poolDictionary;
    }
}
