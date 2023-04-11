using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public InputField playerNameInput;

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int score;
    }

    private PlayerData playerData;


    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit(); 
#endif
    }

    public int LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<int>(json);
        }

        return 0;
    }

    public void SavePlayerData(string playerName, int score)
    {
        // Load the high score from file
        int highScore = LoadHighScore();

        // If the current score is higher than the high score, update it
        if (score > highScore)
        {
            highScore = score;

            // Save the new high score to file
            string highScoreJson = JsonUtility.ToJson(highScore);
            File.WriteAllText(Application.persistentDataPath + "/highScore.json", highScoreJson);
        }

        PlayerData data = new PlayerData();
        data.playerName = playerName;
        data.score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);

        // Update the player data in memory
        playerData = data;
    }

    public PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerData>(json);
        }

        return null;
    }
}
