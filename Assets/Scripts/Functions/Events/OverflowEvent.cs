using System.Collections;
using UnityEngine;

public class OverflowEvent : MonoBehaviour
{
    [SerializeField] private Transform DamagingLiquid;
    [SerializeField] private Transform PointLow; // Inactive event position
    [SerializeField] private Transform PointHigh; // Active event position

    public float speed = 2.5f;

    public bool isOverflow = false;
    private bool doesRandomHaveToStop = false;
    public IEnumerator Event()
    {
        OverallEvents.IsEventRunning = true;
        StartCoroutine(RaiseDamagingFluid());
        // start music
        yield return new WaitForSeconds(50);
        StartCoroutine(LowerDamagingFluid());
    }
    private IEnumerator RaiseDamagingFluid()
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

    private IEnumerator LowerDamagingFluid()
    {
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
    public bool DEV_ForceOverflow()
    {
        if (!OverallEvents.IsEventRunning && !OverallEvents.IsMainEventRunning)
        {
            StartCoroutine(Event());
            isOverflow = true;
            return true;
        }
        else
        {
            Debug.LogWarning("Event is running, cannot start Overflow event");
            return false;
        }
    }
    public void RandomEventStart()  
    {
        //Debug.Log("Random blackouts will occour again.");
        while (!doesRandomHaveToStop)
        {
            int willEventStart = Random.Range(0, 150);
            if (willEventStart == 67 && !OverallEvents.IsEventRunning && !OverallEvents.IsMainEventRunning)
            {
                isOverflow = true;
                StartCoroutine(Event());
            }
        }
    }
}