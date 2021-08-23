using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreCheck : MonoBehaviour
{
    public Text highScoreCheck;
    
    void Update()
    {
        int highScore = PlayerPrefs.GetInt("HIGH SCORE");
        highScoreCheck.text = highScore.ToString("000");
    }
}
