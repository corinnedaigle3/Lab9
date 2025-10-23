using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISaveable
{
    string SaveID { get; }
    JsonData SavedData { get; }
    void LoadFromData(JsonData data);
}

public static class SavingService
{
    private const string ACTIVE_SCENE_KEY = "activeScene";
    private const string SCENES_KEY = "scenes";
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    // Keep this delegate for delayed object loading
    private static SceneManager.SceneLoadedCallback LoadObjectsAfterSceneLoad;

    public static void SaveGame(string fileName)
    {
        var result = new JsonData();
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        if (allSaveableObjects.Any())
        {
            var savedObjects = new JsonData();
            savedObjects.SetJsonType(JsonType.Array);

            foreach (var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;
                if (data.IsObject)
                {
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    savedObjects.Add(data);
                }
                else
                {
                    var behaviour = saveableObject as MonoBehaviour;
                    Debug.LogWarningFormat(behaviour, "{0}'s save data is not a dictionary. The object was not saved.", behaviour.name);
                }
            }
            result[OBJECTS_KEY] = savedObjects;
        }
        else
        {
            Debug.LogWarning("The scene did not include any saveable objects.");
        }

        // Save scenes
        var openScenes = new JsonData();
        openScenes.SetJsonType(JsonType.Array);
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            openScenes.Add(scene.name);
        }

        result[SCENES_KEY] = openScenes;
        result[ACTIVE_SCENE_KEY] = SceneManager.GetActiveScene().name;

        // Write to disk
        var outputPath = Path.Combine(Application.persistentDataPath, fileName);
        var writer = new JsonWriter { PrettyPrint = true };
        result.ToJson(writer);

        File.WriteAllText(outputPath, writer.ToString());
        Debug.LogFormat("Wrote saved game to {0}", outputPath);

        result = null;
        System.GC.Collect();
    }

    public static bool LoadGame(string fileName)
    {
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(dataPath))
        {
            Debug.LogErrorFormat("No file exists at {0}", dataPath);
            return false;
        }

        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);

        if (data == null || !data.IsObject)
        {
            Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath);
            return false;
        }

        if (!HasKey(data, SCENES_KEY))
        {
            Debug.LogWarningFormat("Data at {0} does not contain any scenes; not loading any!", dataPath);
            return false;
        }

        var scenes = data[SCENES_KEY];
        int sceneCount = scenes.Count;

        if (sceneCount == 0)
        {
            Debug.LogWarningFormat("Data at {0} doesn't specify any scenes to load.", dataPath);
            return false;
        }

        for (int i = 0; i < sceneCount; i++)
        {
            var scene = (string)scenes[i];
            if (i == 0)
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            else
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        // Active Scene
        if (HasKey(data, ACTIVE_SCENE_KEY))
        {
            var activeSceneName = (string)data[ACTIVE_SCENE_KEY];
            var activeScene = SceneManager.GetSceneByName(activeSceneName);
            if (!activeScene.IsValid())
            {
                Debug.LogErrorFormat("Data at {0} specifies an active scene that doesn't exist. Stopping loading here.", dataPath);
                return false;
            }
            SceneManager.SetActiveScene(activeScene);
        }
        else
        {
            Debug.LogWarningFormat("Data at {0} does not specify an active scene.", dataPath);
        }

        // Load objects
        if (HasKey(data, OBJECTS_KEY))
        {
            var objects = data[OBJECTS_KEY];

            LoadObjectsAfterSceneLoad = (scene, loadSceneMode) =>
            {
                var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>()
                    .OfType<ISaveable>()
                    .ToDictionary(o => o.SaveID, o => o);

                for (int i = 0; i < objects.Count; i++)
                {
                    var objectData = objects[i];
                    var saveID = (string)objectData[SAVEID_KEY];
                    if (allLoadableObjects.ContainsKey(saveID))
                    {
                        allLoadableObjects[saveID].LoadFromData(objectData);
                    }
                }

                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                LoadObjectsAfterSceneLoad = null;
                System.GC.Collect();
            };

            SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
        }

        return true;
    }

    private static bool HasKey(JsonData data, string key)
    {
        if (!data.IsObject) return false;
        return ((IDictionary)data).Contains(key);
    }
}