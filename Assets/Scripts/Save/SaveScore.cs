using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int score;
}

public static class SaveScore
{
    private static string fileName = "score.json";

    public static void SavingScore(int score)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(path))
        {
            ScoreData data = new ScoreData { score = score };
            bf.Serialize(file, data);
        }
        Debug.Log($"Score saved to {path}");
    }

    public static int LoadScore()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("No score file found, returning 0.");
            return 0;
        }

        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Open(path, FileMode.Open))
        {
            ScoreData data = (ScoreData)bf.Deserialize(file);
            return data.score;
        }
    }
}
