using UnityEngine;

public class Target : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;
    public SaveManager saveManager;

    public void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
    }

    public void Update()
    {
        //Destory targets when loading
        if (saveManager.savedDestroy == true)
        {
            Destroy(gameObject);
            saveManager.savedDestroy = false;
            Debug.Log("Destroyed");
        }
    }

    public void Initialize(float moveSpeed)
    {
        speed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        // Move continuously to the right
        rb.linearVelocity = Vector2.right * speed;

        // Destroy after 10 seconds
        Destroy(gameObject, 5f);
    }
}