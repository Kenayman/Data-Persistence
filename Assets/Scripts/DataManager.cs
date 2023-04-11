using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int highestPoints;
    public string highestName;

    public string inputName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighscore();
    }

    [System.Serializable]
    class SaveData
    {
        public string highestName;
        public int highestPoints;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.highestName = highestName;
        data.highestPoints = highestPoints; ;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestName = data.highestName;
            highestPoints = data.highestPoints;
        }
    }
}
