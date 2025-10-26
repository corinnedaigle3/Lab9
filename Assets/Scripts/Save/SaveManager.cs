using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string saveFileName = "gameSave.json";
    [SerializeField] private ScoreSystem scoreSystem;
    public bool savedDestroy;
    bool isSaving;
    bool isLoading;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isSaving) 
            {
                Debug.LogWarning("saving is happening");

                SaveGame();
            }
        }
        if (Input.GetKeyUp(KeyCode.S)) 
        {
            Debug.LogWarning("Key s is up ");
            isSaving = false;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            savedDestroy = true;
            if (!isLoading)
            {
                LoadGame();

            }
        }
        if (Input.GetKeyUp(KeyCode.L)) 
        { 
            isLoading = false;  
        }
    
    }

    private void SaveGame()
    {
        isSaving = true;
        // Save positions
        SavingService.SaveGame(saveFileName);
        // Save score
        scoreSystem.Save_Score();
        Debug.Log("Game saved successfully!");
    }


    private void LoadGame()
    {
        isLoading = true;
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