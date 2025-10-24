using LitJson;
using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private string saveID;
    public string SaveID => saveID;

    public JsonData SavedData
    {
        get
        {
            JsonData data = new JsonData();
            data["x"] = transform.position.x;
            data["y"] = transform.position.y;
            data["z"] = transform.position.z;
            return data;
        }
    }

    public void LoadFromData(JsonData data)
    {
        if (data == null) return;
        float x = (float)(double)data["x"];
        float y = (float)(double)data["y"];
        float z = (float)(double)data["z"];
        transform.position = new Vector3(x, y, z);
    }
}
