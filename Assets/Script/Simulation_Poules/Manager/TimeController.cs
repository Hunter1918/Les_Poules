using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TimeController : MonoBehaviour
{
    private bool isFastForward = false;
    public float FastTime = 5f;

    public int days = 0;
    public int hours = 0;
    public int minutes = 0;
    public float seconds = 0f;

    public float secondsPerGameDay = 86400f; 

    private float elapsedTime = 0f;

    public TMP_Text timerText; 

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float totalSeconds = elapsedTime;

        days = (int)(totalSeconds / secondsPerGameDay);
        totalSeconds -= days * secondsPerGameDay;

        hours = (int)(totalSeconds / 3600);  
        totalSeconds -= hours * 3600;

        minutes = (int)(totalSeconds / 60);  
        totalSeconds -= minutes * 60;

        seconds = totalSeconds;  

        //Debug.Log(string.Format("Day: {0} Time: {1:00}:{2:00}:{3:00}", days, hours, minutes, seconds));

        if (timerText != null)
        {
            timerText.text = string.Format("Day: {0} Time: {1:00}:{2:00}:{3:00}", days, hours, minutes, (int)seconds);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleFastForward();
        }
    }


    void ToggleFastForward()
    {
        if (isFastForward)
        {
            Time.timeScale = 1f;
            isFastForward = false;
        }
        else
        {
            Time.timeScale = FastTime;
            isFastForward = true;
        }
    }
}
