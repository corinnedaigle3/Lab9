using System.Collections;
using UnityEngine;

/// Manages saving and loading of the game state (positions, scores, etc.).
/// Handles input for saving/loading and coordinates with the SavingService.
public class SaveManager : MonoBehaviour
{
    [Header("Save Configuration")]
    [SerializeField] private string saveFileName = "gameSave.json"; // Name of the file where data will be stored
    [SerializeField] private ScoreSystem scoreSystem;               // Reference to the score management system

    [Header("Debug / State")]
    public bool savedDestroy; // Used by other systems to determine if save/load affects object destruction
    private bool isSaving;    // Prevents multiple saves from triggering at once
    private bool isLoading;   // Prevents multiple loads from triggering at once

    /// Listens for user input to trigger Save (S key) or Load (L key).
    private void Update()
    {
        // --- SAVE ---
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Only allow one save process at a time
            if (!isSaving)
            {
                Debug.LogWarning("Saving is happening...");
                SaveGame();
            }
        }

        // Reset the save flag when the key is released
        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.LogWarning("Key S released.");
            isSaving = false;
        }

        // --- LOAD ---
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Flag that loading may destroy or overwrite objects
            savedDestroy = true;

            // Only allow one load process at a time
            if (!isLoading)
            {
                LoadGame();
            }
        }

        // Reset the load flag when the key is released
        if (Input.GetKeyUp(KeyCode.L))
        {
            isLoading = false;
        }
    }

    /// Saves all game data (positions, scores, etc.) to disk.
    private void SaveGame()
    {
        isSaving = true;

        // Save all objects implementing ISaveable through the SavingService
        SavingService.SaveGame(saveFileName);

        // Save the score using the ScoreSystem
        scoreSystem.Save_Score();

        Debug.Log("Game saved successfully!");
    }


    /// Loads all game data from disk (positions, scores, etc.).
    private void LoadGame()
    {
        isLoading = true;

        // Load saved object data
        bool loaded = SavingService.LoadGame(saveFileName);
        if (loaded)
        {
            Debug.Log("Positions loaded!");
        }

        // Load score data
        scoreSystem.Load_Score();

        Debug.Log("Game loaded successfully!");
    }
}
