using System.Collections;
using UnityEngine;

public class OverflowEvent : MonoBehaviour
{
    [SerializeField] private OverallEvents OverallEvents;

    [SerializeField] private Transform DamagingLiquid;
    [SerializeField] private Transform PointLow; // Inactive event position
    [SerializeField] private Transform PointHigh; // Active event position

    public float speed = 1.0f;

    public bool isOverflow = false;
    private bool doesRandomHaveToStop = false;
    public IEnumerator Event()
    {
        OverallEvents.IsEventRunning = true;
        yield return new WaitForSeconds(1);
    }
    private IEnumerator RaiseDamagingFluid()
    {
        // start music
        if (isOverflow)
        {
            while (Vector3.Distance(DamagingLiquid.position, PointHigh.position) > 0.05f)
            {
                DamagingLiquid.position = Vector3.MoveTowards(DamagingLiquid.position, PointHigh.position, speed * Time.deltaTime);
                if (DamagingLiquid.position == PointHigh.position)
                {
                    break;
                }
                yield return null;
            }
        }

        yield return new WaitForSeconds(10);

        while (Vector3.Distance(DamagingLiquid.position, PointLow.position) > 0.05f)
        {
            DamagingLiquid.position = Vector3.MoveTowards(DamagingLiquid.position, PointLow.position, speed * Time.deltaTime);
            if (DamagingLiquid.position == PointLow.position)
            {
                break;
            }
            yield return null;
        }
    }
    /// <summary>
    /// Force to start the event.
    /// </summary>
    public void EventStart()
    {
        if (!OverallEvents.IsEventRunning && !OverallEvents.IsMainEventRunning)
        {
            StartCoroutine(Event());
            isOverflow = true;
        }
        else
        {
            Debug.LogWarning("Event is running, cannot start Overflow event");
        }
    }
    public void randomEventStart()
    {
        //Debug.Log("Random blackouts will occour again.");
        while (!doesRandomHaveToStop)
        {
            int willEventStart = Random.Range(0, 150);
            if (willEventStart == 69 && !OverallEvents.IsEventRunning && !OverallEvents.IsMainEventRunning)
            {
                StartCoroutine(Event());
                isOverflow = true;
            }
        }
    }
}
