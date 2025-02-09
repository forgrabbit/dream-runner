using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float minTime;
}

public class Record : MonoBehaviour
{
    public SaveData saveData = new SaveData();

    public void SaveGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }
}