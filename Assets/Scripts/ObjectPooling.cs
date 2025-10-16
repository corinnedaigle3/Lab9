using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public GameObject bullet;
    public int poolAmount = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        SharedInstance = this;
    }
    void Start()
    {
        for(int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public GameObject GetBullet()
    {
        if(pool.Count > 0)
        {
            Debug.Log("Got bullet");
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null; 
    }
    public void DisableBullet(GameObject obj)
    {
        Debug.Log("Disabled bullet");
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
