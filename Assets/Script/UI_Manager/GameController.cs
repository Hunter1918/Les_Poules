using UnityEngine;

public class GameController : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float secondsPerGameDay = 86400f;
    public int days = 0;
    public int hours = 0;
    public int minutes = 0;
    public float seconds = 0f;

    void Update()
    {
        UpdateGameTime();
    }

    private void UpdateGameTime()
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
    }

    public void OnPause()
    {
        Debug.Log("Game is paused.");
    }

    public void OnResume()
    {
        Debug.Log("Game resumed.");
    }
}