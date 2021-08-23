using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float totalTime;
    public GameObject gameOver;

    void Update()
    {
        totalTime -= Time.deltaTime;
        int minute = (int)totalTime / 60;
        int second = (int)totalTime % 60;
        timerText.text = minute.ToString("00") + ":" + second.ToString("00");

        if(totalTime <= 0)
        {
            totalTime = 0 ;
            gameOver.gameObject.SetActive(true);
        }
    }
}
