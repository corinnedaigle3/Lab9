using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private float bulletLife;

    private GameObject target;
    private Subject subject;

    private void Start()
    {
        target = GameObject.FindWithTag("Enemy");
        subject = target.GetComponent<Subject>();
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
            //Debug.Log("bullet collided");
            if (target != null) 
            {
                subject.Hit();
            }

            ObjectPooling.SharedInstance.DisableBullet(gameObject);
        }
    }
}
