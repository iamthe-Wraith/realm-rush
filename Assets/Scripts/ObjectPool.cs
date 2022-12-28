using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void EnabledObjectInPool()
    {
        GameObject objToEnable = Array.Find(pool, obj => !obj.activeInHierarchy);

        if (objToEnable != null)
        {
            objToEnable.SetActive(true);
        }
    }
    

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }
    

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnabledObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }   
    }
}