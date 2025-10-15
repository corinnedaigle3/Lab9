using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject bullet;
    public float moveSpeed;
    void Start()
    {
       // bullet = ObjectPooling.SharedInstance.GetPooledObject();
        moveSpeed = 10f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Move right");
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Move left");
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Shoot");
            bullet.SetActive(true);
        }
    }
}
