using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] ScoreType type;

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ScoreType.current:
                GetComponent<Text>().text = $"Score: {GameObject.Find("GameManager").GetComponent<Score>().CurrentScore}";
                break;
            case ScoreType.high:
                GetComponent<Text>().text = $"High Score: {GameObject.Find("GameManager").GetComponent<Score>().highestScore}";
                break;
        }
        
    }

    public enum ScoreType
    {
        current,
        high
    }
}
