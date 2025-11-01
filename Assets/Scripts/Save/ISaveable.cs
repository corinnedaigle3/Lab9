using LitJson;                        // For working with JSON data (serialization/deserialization)
using System.IO;                      // For file reading/writing
using System.Linq;                    // For working with collections (e.g., filtering or mapping)
using System.Threading.Tasks;         // For async background saving
using UnityEngine;

// Interface defining what makes an object "saveable"
public interface ISaveable
{
    string SaveID { get; }            // Unique identifier for each object (used to match on load)
    JsonData SavedData { get; }       // The object's data converted to JSON
    void LoadFromData(JsonData data); // Method to restore data from JSON
}

// Static class that manages saving and loading
public static class SavingService
{
    // JSON keys for organizing save file structure
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    // --- SAVE GAME LOGIC ---

    // Async method to save game data (so it doesn’t freeze gameplay)
    public static async void SaveGame(string fileName)
    {
        // Find all active objects in the scene that implement ISaveable
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();

        // Root JSON structure
        var result = new JsonData();
        var savedObjects = new JsonData();
        savedObjects.SetJsonType(JsonType.Array);

        // Loop through all saveable objects and store their data
        foreach (var saveableObject in allSaveableObjects)
        {
            var data = saveableObject.SavedData;

            // Skip invalid or empty data
            if (data == null || !data.IsObject)
                continue;

            // Add unique save ID to the JSON object
            data[SAVEID_KEY] = saveableObject.SaveID;

            // Add this object's JSON data to the list
            savedObjects.Add(data);
        }

        // Assign the array of saved objects to the main JSON object
        result[OBJECTS_KEY] = savedObjects;

        // Define where the file will be saved (persistent across sessions)
        var outputPath = Path.Combine(Application.persistentDataPath, fileName);

        // Run the file-writing process in a background thread (doesn't block gameplay)
        await Task.Run(() =>
        {
            var writer = new JsonWriter { PrettyPrint = true }; // Makes JSON human-readable
            result.ToJson(writer);
            File.WriteAllText(outputPath, writer.ToString());   // Write JSON to disk
        });
    }

    // --- LOAD GAME LOGIC ---

    public static bool LoadGame(string fileName)
    {
        // Locate the saved file
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);

        // Check if it exists before loading
        if (File.Exists(dataPath) == false)
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return false;
        }
        else
        {
            Debug.Log("Load Game: file exists");
        }

        // Read the JSON text and parse it into a JsonData object
        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);

        // Validate data format
        if (data == null || data.IsObject == false)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath);
            return false;
        }

        // Extract all saved object data
        var objects = data[OBJECTS_KEY];

        // Find all ISaveable objects in the current scene, indexed by SaveID
        var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>()
                                       .OfType<ISaveable>()
                                       .ToDictionary(o => o.SaveID, o => o);

        var objectsCount = objects.Count;

        // Loop through all saved objects and apply their data
        for (int i = 0; i < objectsCount; i++)
        {
            var objectData = objects[i];
            var saveID = (string)objectData[SAVEID_KEY];

            // If an object with this SaveID exists in the scene, load its data
            if (allLoadableObjects.ContainsKey(saveID))
            {
                var loadableObject = allLoadableObjects[saveID];
                loadableObject.LoadFromData(objectData);
            }
            else
            {
                // Warn if the scene doesn’t contain the object that was saved
                Debug.LogWarning($"No matching object with SaveID '{saveID}' found in scene.");
            }
        }

        // Optional cleanup and memory collection
        System.GC.Collect();
        Debug.LogWarning("Game Loaded Successfully");
        return true;
    }
}
