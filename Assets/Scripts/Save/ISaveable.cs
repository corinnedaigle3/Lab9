using LitJson;
using System.IO;
using System.Linq;
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
}
