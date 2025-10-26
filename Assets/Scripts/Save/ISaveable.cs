using LitJson;
using System.IO;
using System.Linq;
using System.Threading.Tasks; // added threading tasks so the game does not stop while the code is doing something
using UnityEngine;

public interface ISaveable
{
    string SaveID { get; }
    JsonData SavedData { get; }
    void LoadFromData(JsonData data);
}
public static class SavingService
{
    //private const string ACTIVE_SCENE_KEY = "activeScene";
    //private const string SCENES_KEY = "scenes"; 
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    // made the SaveGame async so it can use tasks
    public static async void SaveGame(string fileName)
    {
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();

        var result = new JsonData();
        var savedObjects = new JsonData();
        savedObjects.SetJsonType(JsonType.Array);

        foreach (var saveableObject in allSaveableObjects)
        {
            var data = saveableObject.SavedData;
            if (data == null || !data.IsObject)
            {
                continue;
            }

            data[SAVEID_KEY] = saveableObject.SaveID;
            savedObjects.Add(data);
        }

        result[OBJECTS_KEY] = savedObjects;

  

        var outputPath = Path.Combine(Application.persistentDataPath, fileName);
        await Task.Run(() => // This task is bing executed without stopping the game 
        {
            var writer = new JsonWriter { PrettyPrint = true };
            result.ToJson(writer);
            File.WriteAllText(outputPath, writer.ToString());
        });

    }

    public static bool LoadGame(string fileName)
    {
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(dataPath) == false)
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath); return false;
        }
        else
        {
            Debug.Log("Load Game: file exists");
        }

        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);

        if (data == null || data.IsObject == false)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath); return false;
        }

    
            var objects = data[OBJECTS_KEY];
            var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o); 
            var objectsCount = objects.Count;
            for (int i = 0; i < objectsCount; i++)
            {
                var objectData = objects[i];
                var saveID = (string)objectData[SAVEID_KEY];
                if (allLoadableObjects.ContainsKey(saveID))
                {
                    var loadableObject = allLoadableObjects[saveID];
                    loadableObject.LoadFromData(objectData);
                }
                else
                {
                    Debug.LogWarning($"No matching object with SaveID '{saveID}' found in scene.");
                }
            }
            /*                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                            LoadObjectsAfterSceneLoad = null;*/
            System.GC.Collect();
            Debug.LogWarning("Game Loaded Successfully");
            return true;
        //};
            //SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
    }
        
}




