using System.IO;
using UnityEngine;
using Unity.Services.Leaderboards;

[System.Serializable]
public class SaveData
{
    public int score = 0;
    public bool hasScore = false;
}

public class Record : MonoBehaviour
{
    public SaveData saveData = new SaveData();

    public void SaveGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(saveData, true);
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }
}