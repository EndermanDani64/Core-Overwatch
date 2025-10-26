using System.Collections;
using UnityEngine;

public class OverflowEvent : MonoBehaviour
{
    public static bool IsOverflowEvent = false;

    [SerializeField] private Transform DamagingLiquid;
    [SerializeField] private Transform PointLow; // Inactive event position
    [SerializeField] private Transform PointMaxHigh; // Maximum height that the DamagingLiquid can go up

    public float speed = 0f; // 1 piece of dp. adds to the speed +.36f

    public int difficulty = 0; // 0: 0 damaged pipe | 1: 1 dp. | 2: 2 dp. | ... max 5

    public bool isOverflow = false;
    // private bool doesRandomHaveToStop = false; DEPRICATED, COULD USE THE isFixablePipeAvalible INSTEAD

    public GameObject[] fixablePipeList = { };
    [SerializeField] public static int fixablePipeAvalibleCount = 0;

    private void Awake()
    {
        fixablePipeList = GameObject.FindGameObjectsWithTag("FixablePipe");
    }

    public IEnumerator Event()
    {
        OverallEvents.IsEventRunning = true;
        IsOverflowEvent = true;
        DamageRandomPipe(difficulty);
        yield return new WaitForSeconds(1);
        StartCoroutine(RaiseDamagingFluid());
        // start music

        while (fixablePipeAvalibleCount > 0)
        {
            OverallEvents.IsEventRunning = true;
            yield return new WaitForSeconds(1);
        }

        OverallEvents.IsEventRunning = false;
        IsOverflowEvent = false;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(LowerDamagingFluid_FixedPoint());
    }

    private void DamageRandomPipe(int piece)
    {
        while (piece > 0)
        {
            int random = UnityEngine.Random.Range(0, fixablePipeList.Length);

            if (fixablePipeList[random].GetComponent<OverflowEvent_FixablePipe>().isFixed) // if the pipe is fixed then damage it
            {
                fixablePipeList[random].GetComponent<OverflowEvent_FixablePipe>().DamagePipe();
                fixablePipeAvalibleCount++;

                speed += ValueStorage.OVERFLOWEVENT_LIQUIDSPEED_INCRESE_PERPIPE;

                piece--;
            }
        }
    }

    private float timeHeld = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && timeHeld < ValueStorage.TIME_FIXABLE_PIPE_TIMETOFIX && fixablePipeAvalibleCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("FixablePipe") && !hit.collider.GetComponent<OverflowEvent_FixablePipe>().isFixed)
                {
                    bool isFixed = hit.collider.GetComponent<OverflowEvent_FixablePipe>().isFixed;
                    Debug.Log($"timeheld = {Mathf.Round(timeHeld)} | isFixed = {isFixed}");

                    if (Mathf.Round(timeHeld) >= ValueStorage.TIME_FIXABLE_PIPE_TIMETOFIX && !isFixed)
                    {
                        Debug.Log("fixed!");
                        hit.collider.GetComponent<OverflowEvent_FixablePipe>().isFixed = true;
                        //hit.collider.GetComponent<ParticleSystem>().startLifetime = 0;
                        hit.collider.GetComponent<OverflowEvent_FixablePipe>().Sound();

                        fixablePipeAvalibleCount--;
                        timeHeld = 0f;
                    }
                    else if (timeHeld < ValueStorage.TIME_FIXABLE_PIPE_TIMETOFIX && !isFixed)
                    {
                        timeHeld += Time.deltaTime;
                    }
                }
            }
        }
        /*else
        {
            Debug.Log("elengedve");
            timeHeld = 0f;
        }*/

        if (!Input.GetKey(KeyCode.E) && fixablePipeAvalibleCount > 0 && timeHeld != 0) // if the player releases the button [E] then 
        {
            Debug.Log("elengedve");
            timeHeld = 0f;
        }
    }

    /// <summary>
    /// Raises the DamagingObject in the map while there is fixable pipe.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RaiseDamagingFluid()
    {
        while (Vector3.Distance(DamagingLiquid.position, PointMaxHigh.position) > 0.05f && fixablePipeAvalibleCount > 0)
        {
            DamagingLiquid.position = Vector3.MoveTowards(DamagingLiquid.position, PointMaxHigh.position, speed * Time.deltaTime);
            if (DamagingLiquid.position == PointMaxHigh.position) 
            {
                break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// [DEPRICATED] Raises the DamagingObject in the map to a fixed point.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RaiseDamagingFluid_FixedPoint()
    {
        while (Vector3.Distance(DamagingLiquid.position, PointMaxHigh.position) > 0.05f)
        {
            DamagingLiquid.position = Vector3.MoveTowards(DamagingLiquid.position, PointMaxHigh.position, speed * Time.deltaTime);
            if (DamagingLiquid.position == PointMaxHigh.position)
            {
                break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Lowers the DamagingObject in the map to a fixed point.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LowerDamagingFluid_FixedPoint()
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

    /*public void RandomEventStart()  
    {
        //Debug.Log("Random blackouts will occour again.");
        while (!doesRandomHaveToStop && !OverallEvents.IsEventRunning && !OverallEvents.IsMainEventRunning)
        {
            int willEventStart = Random.Range(0, 150);
            if (willEventStart == 67 && !OverallEvents.IsEventRunning && !OverallEvents.IsMainEventRunning)
            {
                isOverflow = true;
                StartCoroutine(Event());
            }
        }
    }*/
}