using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> pooledBullets;
    public GameObject bulletToPool;
    public int poolAmount;

    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject tmp;

        for(int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(bulletToPool);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }
        
    }


    public GameObject GetPooledObject()
    {
        for(int i = 0; i < poolAmount; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }

        return null;
        
    }
}
