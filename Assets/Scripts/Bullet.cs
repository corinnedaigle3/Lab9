using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject bullet;
    void Start()
    {
        bullet = ObjectPooling.SharedInstance.GetPooledObject();

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) //If collides with enemy send bullet back to pool
        {
            bullet.SetActive(false);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5.0f);
        bullet.SetActive(false);
    }

}
