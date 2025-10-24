using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private TransformSaver transformSaver;

    [SerializeField] private ScoreSystem scoreSystem;

    void Start()
    {
        moveSpeed = 10f;

        transformSaver = GetComponent<TransformSaver>();
        if (transformSaver == null)
        {
            Debug.LogError("No TransformSaver component found on this GameObject!");
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Move right");
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Move left");
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Save Game");
            SavingService.SaveGame("save.json");
            scoreSystem.Save_Score();
        }
        if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("Load Game");
            SavingService.LoadGame("save.json");
            scoreSystem.Load_Score();
        }
    }
}
