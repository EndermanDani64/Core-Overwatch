using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] TempController tempController;

    public float elapsedTimeInSeconds = 0;
    public int minutes = 0;
    public int hour = 12;

    public static float timeScale = 6f;

    private int tempHour = 0;

    void Update()
    {
        elapsedTimeInSeconds += Time.deltaTime * timeScale;

        minutes = Mathf.FloorToInt(elapsedTimeInSeconds) % 60;
        hour = (23 + (Mathf.FloorToInt(elapsedTimeInSeconds) / 60) % 24) % 24;

        if (hour == 12 && tempHour != 12 && tempController.isOnline)
        {
            scoreManager.WorkshiftEnd("Night");
        }
        else if (hour == 0 && tempHour != 0 && tempController.isOnline)
        {
            scoreManager.WorkshiftEnd("Day");
        }
            tempHour = hour;

        Debug.Log($"{hour.ToString("00")}:{minutes.ToString("00")}");
    }
}
