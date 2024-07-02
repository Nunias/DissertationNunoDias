using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainigTime;

    // Update is called once per frame
    void Update()
    {
        if (remainigTime > 0)
        {
            remainigTime -= Time.deltaTime;
        }

        if (remainigTime < 0)
        {
            remainigTime = 0;
            SceneManager.LoadSceneAsync("End Menu");
        }

        int minutes = Mathf.FloorToInt(remainigTime / 60);
        int seconds = Mathf.FloorToInt(remainigTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
