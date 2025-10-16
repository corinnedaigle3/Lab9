using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private float bulletLife;

    void Update()
    {
        bulletLife += Time.deltaTime;

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if( bulletLife > 3f)
        {
            Debug.Log("3 seconds up");
            ObjectPooling.SharedInstance.DisableBullet(gameObject);
            bulletLife = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Debug.Log("bullet collided");
            ObjectPooling.SharedInstance.DisableBullet(gameObject);
        }
    }
}
