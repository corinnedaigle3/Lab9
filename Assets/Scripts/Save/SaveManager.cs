using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string saveFileName = "gameSave.json";
    [SerializeField] private ScoreSystem scoreSystem;
    public bool savedDestroy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            savedDestroy = true;
            LoadGame();
        }
    }

    private void SaveGame()
    {
        // Save positions
        SavingService.SaveGame(saveFileName);
        // Save score
        scoreSystem.Save_Score();
        Debug.Log("Game saved successfully!");
    }

    private void LoadGame()
    {
        // Load positions
        bool loaded = SavingService.LoadGame(saveFileName);
        if (loaded)
        {
            Debug.Log("Positions loaded!");
        }

        // Load score
        scoreSystem.Load_Score();
        Debug.Log("Game loaded successfully!");
    }
}