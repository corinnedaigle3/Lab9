using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public Transform spawnPoint;

    private GameObject bullet;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            bullet = ObjectPooling.SharedInstance.GetBullet();
            Debug.Log("Shoot");

            if (bullet != null)
            {
                bullet.transform.position = spawnPoint.transform.position;
                bullet.transform.rotation = spawnPoint.transform.rotation;
            }
        }

    }
}
