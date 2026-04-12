using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAudioEmitter : MonoBehaviour 
{
    /// <summary>PlayerSoundEmmiter 
    /// Takes an event's name and plays the event on the Studio Event Emmiter.
    /// The event names are all small caps.
    /// </summary>
    public void PlaySound(string eventName)
    {
        bool didChange = false;
        
        foreach (KeyValuePair<string, StudioEventEmitter> pair in _emitterReferences)
        {
            if (pair.Key == eventName)
            {
                pair.Value.Play();
                _activeEmitters.Add(pair.Value);
                didChange = true;
            }
        }

        if (!didChange) Debug.LogWarning($"Didn't find any event by the name \"{eventName}\"");
    }

    public void StopSounds()
    {
        foreach (StudioEventEmitter emitter in _activeEmitters)
        {
            emitter.Stop();
        }
        _activeEmitters.Clear();
    }

    private void Start()
    {
        _emitterReferences = new Dictionary<string, StudioEventEmitter>()
        {
            ["ms_shiftend"] = _ms_shiftend,
            ["ms_beforemeltdown"] = _ms_beforemeltdown,
            ["ms_aftermeltdown"] = _ms_aftermeltdown,
            ["ms_overflow"] = _ms_overflow,
            ["ms_blackout"] = _ms_blackout,
            ["fx_blackout"] = _fx_blackout
        };

        _overallEvents.Meltdown_FirstSegment_Start += () =>
        {
            StopSounds();
            _ms_beforemeltdown.Play();
        };
        _overallEvents.Meltdown_SecondSegment_Start += () =>
        {
            StopSounds();
            _ms_aftermeltdown.Play();
        };
        _overallEvents.Blackout_Start += BlackoutStart;
    }

    private IEnumerator BlackoutStart()
    {
        StopSounds();
        _fx_blackout.Play();
        yield return new WaitForSeconds(1.5f);
        _ms_blackout.Play();
        Debug.Log("sounds play");
    }

    // declarations

    [SerializeField] private OverallEvents _overallEvents;
    [SerializeField] private StudioEventEmitter _ms_shiftend;
    [SerializeField] private StudioEventEmitter _ms_beforemeltdown; // rename these to firstsegment and secondsegment, in FMOD as well
    [SerializeField] private StudioEventEmitter _ms_aftermeltdown;
    [SerializeField] private StudioEventEmitter _ms_overflow;
    [SerializeField] private StudioEventEmitter _fx_blackout;
    [SerializeField] private StudioEventEmitter _ms_blackout;
    /// <summary>
    /// The event names are all small caps!!
    /// </summary> 
    private List<StudioEventEmitter> _activeEmitters = new();
    private Dictionary<string, StudioEventEmitter> _emitterReferences;
}
