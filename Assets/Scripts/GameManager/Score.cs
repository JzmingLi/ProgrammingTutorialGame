using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Score : MonoBehaviour
{
    private int _currentScore;
    public int CurrentScore
    {
        get => _currentScore; 
        set => _currentScore = value;
    }

    public int highestScore;
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/HighestScore.json";
        LoadHighScore();
}

    public void SaveHighScore()
    {
        SaveData data = new SaveData(highestScore);

        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(JsonUtility.ToJson(data,true));
            }
        }
        Debug.Log($"Saved to {savePath}");
    }

    public void LoadHighScore()
    {
        SaveData data = new SaveData(0);
        if (File.Exists(savePath))
        {
            using (FileStream stream = new FileStream(savePath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    data = JsonUtility.FromJson <SaveData>(reader.ReadToEnd());
                }
                highestScore = data.highscore;
                Debug.Log($"Loaded from {savePath}");
            }
        }
        else
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        }
    }

}
