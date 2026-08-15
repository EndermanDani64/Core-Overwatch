using UnityEngine;

public class DisplayTimeManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    public float elapsedTimeInSeconds = 0;
    public int minutes = 0;
    public int hour = 12;

    public static float timeScale = 5f;

    private int tempHour = 0;

    void Update()
    {
        elapsedTimeInSeconds += Time.deltaTime * timeScale;

        minutes = Mathf.FloorToInt(elapsedTimeInSeconds) % 60;
        hour = (12 + (Mathf.FloorToInt(elapsedTimeInSeconds) / 60) % 24) % 24;

        if (hour == 12 && tempHour != 12 && ReactorManager.ReactorData.IsOnline)
        {
            scoreManager.WorkshiftEnd("Night");
        }
        else if (hour == 0 && tempHour != 0 && ReactorManager.ReactorData.IsOnline)
        {
            scoreManager.WorkshiftEnd("Day");
        }

        tempHour = hour;
    }
}
