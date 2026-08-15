using System;
using System.Collections;
using UnityEngine;

public class BlackoutEvent : MonoBehaviour, IEvent
{
    public string EventID { get; set; }
    private Coroutine eventCoroutine;

    public IEnumerator SetPower() // this is called when the randomEvent() has got the randomness
    {
        BlackoutEvent_SatusChange?.Invoke(true);

        playerAudioEmitter.PlaySound("fx_blackout");
        yield return new WaitForSeconds(0.3f);
        playerAudioEmitter.PlaySound("ms_blackout");

        OverallEvents.IsEventRunning = true;
        OverallEvents.IsBlackout = true;

        LightControl.LightOutage();
        //source.PlayOneShot(soundEffect);
        ShoutSystem.SendMessage("Power went, whoossss-");
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
        //StartCoroutine(randomEvent());
        //flashLight.enabled = false;
        BlackoutEvent_SatusChange?.Invoke(false);
    }

    // ----  OverallEvents  ---- //

    public void EventStart() // do not use except OverallEvents
    {
        eventCoroutine = StartCoroutine(SetPower());
    }

    public void EventEnd()
    {
        StopCoroutine(eventCoroutine);
    }    

    // ----  Initiating  ---- //

    public event Action<bool> BlackoutEvent_SatusChange;

    /*[SerializeField] private AudioSource source;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioClip music;*/

    [SerializeField] private OverallEvents overallEvents;
    [SerializeField] private PlayerAudioEmitter playerAudioEmitter;
    [SerializeField] private ShoutSystem ShoutSystem;
    [SerializeField] private FacilityLightsManager LightControl;
}
