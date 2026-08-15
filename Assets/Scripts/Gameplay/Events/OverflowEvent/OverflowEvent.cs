using System;
using System.Collections;
using UnityEngine;

public class OverflowEvent : MonoBehaviour
{
    // ----  Main methods  ---- //

    private float timeEllapsedSinceWaiting = 0f;

    /// <summary>
    /// Used for calling it in loops, so it's random when the event starts.
    /// </summary>
    public IEnumerator RandomEventStart()
    {
        int randomWaitTime = -1;
        IsWaitingForOverflowEvent = true;

        // in case the RandomEventStart has been called and the reactor isn't pressurized
        while (ReactorManager.ReactorData.IsPressurized)
        {
            if (randomWaitTime == -1)
            {
                randomWaitTime = UnityEngine.Random.Range(80, 360); // 80, 360
                Debug.Log($"Selected time for OverflowEvent = {randomWaitTime}");
            }

            if (timeEllapsedSinceWaiting >= randomWaitTime) // If the player waited randomWaitTime seconds...
            {
                int randomInt = UnityEngine.Random.Range(0, Mathf.RoundToInt(ReactorManager.ReactorData.Pressure / 3));

                // Debug.Log($"range: 0-{Mathf.RoundToInt(PressureControl.pressure / 3)} | randomInt = {randomInt} <? {Mathf.RoundToInt(PressureControl.pressure / 5)} | ? {randomInt < Mathf.RoundToInt(PressureControl.pressure / 5)}");

                if (randomInt < Mathf.RoundToInt(ReactorManager.ReactorData.Pressure / 5) && !IsOverflowEventCooldown)
                {
                    difficulty = 4;
                    //StartCoroutine(Event());
                    _overallEvents.PlayEvent("overflow");
                    break;
                }
            }
            else
            {
                timeEllapsedSinceWaiting += 1f;
                Debug.Log($"Untill OverflowEvent = {timeEllapsedSinceWaiting} -> {randomWaitTime}");
            }
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Main method for handling the event, starts the Overflow event.
    /// </summary>
    public IEnumerator Event()
    {
        OverflowEvent_SatusChange?.Invoke(true);
        IsWaitingForOverflowEvent = false;
        OverallEvents.IsOverflow = true;
        OverallEvents.IsEventRunning = true;
        DamageRandomPipe(difficulty);

        yield return new WaitForSeconds(1);
        StartCoroutine(RaiseDamagingFluid());
        _soundSystem.PlaySound("ms_overflow");

        while (fixablePipeAvalibleCount > 0)
        {
            OverallEvents.IsEventRunning = true;
            yield return new WaitForSeconds(1);
        }

        OverallEvents.IsEventRunning = false;
        OverallEvents.IsOverflow = false;

        yield return new WaitForSeconds(.5f);

        StartCoroutine(LowerDamagingFluid_FixedPoint());
        StartCoroutine(EventCooldown());
        _overallEvents.EventQueue_TriggerNext();

        OverflowEvent_SatusChange?.Invoke(false);
    }

    private IEnumerator EventCooldown()
    {
        IsOverflowEventCooldown = true;
        yield return new WaitForSeconds(300);
        IsOverflowEventCooldown = false;
        _overallEvents.EventQueue_TriggerNext();
    }

    // ----  Unity Default Methods  ---- //

    private void Awake()
    {
        fixablePipeList = GameObject.FindGameObjectsWithTag("FixablePipe");
    }

    private float _timeHeld = 0f;
    void Update() // some player interaction checks
    {
        if (Player.LookingAtTag("FixablePipe", KeyCode.E) && fixablePipeAvalibleCount > 0)
        {
            if (_timeHeld >= ValueStorage.TIME_FIXABLE_PIPE_TIMETOFIX)
            {
                OverflowEvent_FixablePipe fixablePipe = Player.GetLookedAtObject().GetComponent<OverflowEvent_FixablePipe>();
                if (!fixablePipe.isFixed) 
                {
                    fixablePipe.FixPipe();
                    fixablePipeAvalibleCount--;
                    _timeHeld = 0f;
                }
            }
            else
            {
                _timeHeld += Time.deltaTime;
                Debug.Log($"timeheld = {Mathf.Round(_timeHeld)} | fixablePipeAvalibleCount = {fixablePipeAvalibleCount}");
            }
        }
        else
        {
            _timeHeld = 0f;
        }
    }

    /// <summary>!!
    /// just thinking abt stuff, nothing serious. trying to connect with the scattered FixablePipes and make an event connection so i don't have to use update()
    /// </summary>
    public void FixPipe(bool isFixed)
    {
        if (Player.LookingAtTag("FixablePipe", KeyCode.E) && !isFixed)
        {
            if (Mathf.Round(_timeHeld) >= ValueStorage.TIME_FIXABLE_PIPE_TIMETOFIX)
            {
                _timeHeld += Time.deltaTime;
            }
            else
            {

            }
        }
        fixablePipeAvalibleCount--;
    }

    // ----  Submethods  ----- // 

    /// <summary>
    /// ! SUBMETHOD : Sould not be called, except from the Event() method.
    /// </summary>
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

    /// <summary>
    /// ! SUBMETHOD : Shoud not be called, except from the Event() method.
    /// Raises the DamagingObject in the map while there is fixable pipe.
    /// </summary>
    private IEnumerator RaiseDamagingFluid()
    {
        Debug.Log("raising object");
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
    /// ! SUBMETHOD : Shoud not be called, except from the Event() method. 
    /// Lowers the DamagingObject in the map to a fixed point.
    /// </summary>
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
    
    // ----  Outside only methods  ---- //

    /// <summary>
    /// Updates the OverflowEvent class's difficulty field.
    /// </summary>
    public void UpdateDifficulty()
    {
        difficulty = Mathf.Clamp(Mathf.RoundToInt((ReactorManager.ReactorData.Pressure / ValueStorage.REACTOR_PS_PRESSURIZED) * 10), 0, 4);
    }

    /// <summary>
    /// Do not call except from OverallEvents!!!
    /// </summary>
    public void ForceOverflow()
    {
        difficulty = 4;
        StartCoroutine(Event());
    }

    /// <summary>
    /// Do not call except from OverallEvents!!!
    /// </summary>
    public void ForceOverflowWait()
    {
        StartCoroutine(RandomEventStart());
    }

    // ----  Initializations  ---- //

    [SerializeField] public bool IsOverflowEventCooldown = false;
    public bool IsWaitingForOverflowEvent = false;

    public float speed = 0f; // 1 piece of dp. adds to the speed +.36f
    public int difficulty = 0; // 0: 0 damaged pipe | 1: 1 dp. | 2: 2 dp. | ... max 5

    public GameObject[] fixablePipeList = { };
    [SerializeField] public int fixablePipeAvalibleCount = 0;

    public event Action<bool> OverflowEvent_SatusChange;

    [SerializeField] private Transform DamagingLiquid;
    [SerializeField] private Transform PointLow; // Inactive event position
    [SerializeField] private Transform PointMaxHigh; // Maximum height that the DamagingLiquid can go up

    [SerializeField] private PlayerAudioEmitter _soundSystem;
    [SerializeField] private OverallEvents _overallEvents;
    [SerializeField] private Fixables fixables;
}