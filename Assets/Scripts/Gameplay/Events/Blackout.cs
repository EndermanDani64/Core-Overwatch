using System;
using System.Collections;
using UnityEngine;

public class BlackoutEvent : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(randomEvent());
    }

    public IEnumerator SetPower() // this is called when the randomEvent() has got the randomness
    {
        BlackoutEvent_SatusChange?.Invoke(true);

        playerAudioEmitter.PlaySound("fx_blackout");
        yield return new WaitForSeconds(0.3f);
        playerAudioEmitter.PlaySound("ms_blackout");

        OverallEvents.IsEventRunning = true;
        OverallEvents.IsBlackout = true;

        StopRandomEventCheck();
        LightControl.LightOutage();
        //source.PlayOneShot(soundEffect);
        ShoutSystem.ShowMessage("Power went, whoossss-");
        /*if (!OverallEvents.IsMeltdown)
        {
            source.PlayOneShot(music);
        }*/

        yield return new WaitForSeconds(123);

        OverallEvents.IsEventRunning = false;
        OverallEvents.IsBlackout = false;

        LightControl.LightRestore();
        ShoutSystem.HideMessage();
        overallEvents.EventQueue_TriggerNext();
        StartCoroutine(randomEvent());
        //flashLight.enabled = false;
        BlackoutEvent_SatusChange?.Invoke(false);
    }
    
    private bool doesRandomHaveToStop = false;
    
    private IEnumerator randomEvent() // ? - cus if it doesn't depend on the player's actions, and it's full random
    {
        //Debug.Log("Random blackouts will occour again.");
        while (!doesRandomHaveToStop)
        {
            int willEventStart = UnityEngine.Random.Range(0, 150);
            if (willEventStart == 69 && !OverallEvents.IsBlackout && !OverallEvents.IsMeltdown && !electricityManagger.isDepletedEnergy)
            {
                overallEvents.PlayEvent("blackout");
            }
            yield return new WaitForSeconds(2);
        }
    }

    
    public bool DEV_ForceBlackout()
    {
        overallEvents.PlayEvent("blackout");
        return true;
    }

    public void ForceStop()
    {   
        LightControl.LightRestore();
        ShoutSystem.HideMessage();
        //source.Stop();
        OverallEvents.IsBlackout = false;
        //source.volume = 0.6f;
        //source.PlayOneShot(soundEffect);
        BlackoutEvent_SatusChange?.Invoke(false);
    }

    public void StopRandomEventCheck()
    {
        doesRandomHaveToStop = true;
        Debug.Log("Random blackouts won't occour again.");
    }

    // ----  OverallEvents using it  ---- //

    public void ForceBlackout() // do not use except OverallEvents
    {
        StartCoroutine(SetPower());
    }

    // ----  Initiating  ---- //

    public event Action<bool> BlackoutEvent_SatusChange;

    /*[SerializeField] private AudioSource source;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioClip music;*/

    [SerializeField] private TempController tempController;
    [SerializeField] private ElectricityManagger electricityManagger;
    [SerializeField] private OverallEvents overallEvents;
    [SerializeField] private PlayerAudioEmitter playerAudioEmitter;
    [SerializeField] private ShoutSystem ShoutSystem;
    [SerializeField] private LightControl LightControl;
}
