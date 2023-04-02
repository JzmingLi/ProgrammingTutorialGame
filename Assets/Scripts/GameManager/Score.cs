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

    readonly string encryptionKey = "noOneWillEverGuessThisTrustMe";
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
                string dataString = JsonUtility.ToJson(data, true);
                dataString = EncryptAndDecrypt(dataString);
                writer.Write(dataString);
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
                    string dataString = reader.ReadToEnd();
                    dataString = EncryptAndDecrypt(dataString);
                    data = JsonUtility.FromJson<SaveData>(dataString);
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
    string EncryptAndDecrypt(string data)
    {
        string newData = "";
        char[] dataCharArray = data.ToCharArray();
        char[] keyCharArray = encryptionKey.ToCharArray();
        for (int i = 0; i < data.Length; i++)
        {
            dataCharArray[i] ^= keyCharArray[i % keyCharArray.Length];
            newData += dataCharArray[i];
        }
        return newData;
    }
}
