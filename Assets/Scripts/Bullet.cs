using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private float bulletLife;
    private Subject target;


    private void Start()
    {

    }

    void Update()
    {
        bulletLife += Time.deltaTime;

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if( bulletLife > 2f)
        {
            Debug.Log("2 seconds up");
            ObjectPooling.SharedInstance.DisableBullet(gameObject);
            bulletLife = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Subject subject = collision.gameObject.GetComponent<Subject>();
            if (subject != null) 
            {
                subject.Hit();
            }

            ObjectPooling.SharedInstance.DisableBullet(gameObject);
        }
    }
}
