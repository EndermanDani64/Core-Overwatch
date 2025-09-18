using UnityEngine;

public class OverallEvents : MonoBehaviour
{
    public static bool IsMainEventRunning = false;
    public static bool IsEventRunning = false;

    private void Update()
    {
        if (IsEventRunning || IsMainEventRunning)
        {
            IsEventRunning = true;
        }
        else
        {
            IsEventRunning = false;
        }
    }
}
