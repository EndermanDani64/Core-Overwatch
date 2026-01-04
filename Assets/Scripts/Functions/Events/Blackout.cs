using System.Collections;
using UnityEngine;

public class BlackoutEvent : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioClip music;

    [SerializeField] private TempController tempController;
    [SerializeField] private ElectricityManagger electricityManagger;
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private ShoutSystem ShoutSystem;
    [SerializeField] private LightControl LightControl;

    [SerializeField] private Meltdown meltdownEvent;

    void Start()
    {
        StartCoroutine(randomEvent());
    }

    public IEnumerator SetPower() // this is called when the randomEvent() has got the randomness
    {   
        Debug.Log("Blackout Event has Started!");

        OverallEvents.IsEventRunning = true;
        OverallEvents.IsBlackout = true;

        StopRandomEventCheck();
        LightControl.LightOutage();
        source.PlayOneShot(soundEffect);
        ShoutSystem.ShowMessage("Power went, whoossss-");
        if (!tempController.isMeltdown)
        {
            source.PlayOneShot(music);
        }

        yield return new WaitForSeconds(123); 

        OverallEvents.IsEventRunning = false;
        OverallEvents.IsBlackout = false;

        LightControl.LightRestore();
        ShoutSystem.HideMessage();
        OverallEvents.EventQueue_TriggerNext();
        Debug.Log("Triggered next event on blackout end.");
        StartCoroutine(randomEvent());
        //flashLight.enabled = false;
    }
    
    private bool doesRandomHaveToStop = false;
    
    private IEnumerator randomEvent() // ? - cus if it doesn't depend on the player's actions, and it's full random
    {
        //Debug.Log("Random blackouts will occour again.");
        while (!doesRandomHaveToStop)
        {
            int willEventStart = Random.Range(0, 150);
            if (willEventStart == 69 && !OverallEvents.IsBlackout && !tempController.isMeltdown && !electricityManagger.isDepletedEnergy)
            {
                OverallEvents.PlayEvent("blackout");
            }
            yield return new WaitForSeconds(2);
        }
    }

    public void ForceBlackout() // do not use except OverallEvents
    {
        StartCoroutine(SetPower());
    }
    public bool DEV_ForceBlackout()
    {
        //StartCoroutine(SetPower());
        OverallEvents.PlayEvent("blackout");
        return true;
    }

    public void ForceStop()
    {   
        LightControl.LightRestore();
        ShoutSystem.HideMessage();
        source.Stop();
        OverallEvents.IsBlackout = false;
        //source.volume = 0.6f;
        //source.PlayOneShot(soundEffect);
    }

    public void StopRandomEventCheck()
    {
        doesRandomHaveToStop = true;
        Debug.Log("Random blackouts won't occour again.");
    }
}
