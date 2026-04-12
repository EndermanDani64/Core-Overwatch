using UnityEngine;
using FMODUnity;
using System;

public class Fixable_LocalStorage : MonoBehaviour
{
    public event System.Action FixTrigger;

    public bool isFixed = false;

    public void FixThis()
    {
        if (isFixed) return;

        ParticleSystem.MainModule particleSystem = gameObject.GetComponent<ParticleSystem>().main;

        isFixed = true;
        //particleSystem.duration = 0;
        StudioEventEmitter[] emmiters = gameObject.GetComponentsInChildren<StudioEventEmitter>();
        foreach (StudioEventEmitter emmiter in emmiters)
        {
            if (emmiter.gameObject.name == "FixEventEmitter")
            {
                emmiter.Play();
            }
        }
        particleSystem.startLifetime = 0;
    }

    public void DamageThis()
    {
        if (!isFixed) return;

        ParticleSystem.MainModule particleSystem = gameObject.GetComponent<ParticleSystem>().main;

        isFixed = false;
        //particleSystem.duration = 0.13f;
        particleSystem.startLifetime = 0.1f;

        StudioEventEmitter[] emmiters = gameObject.GetComponentsInChildren<StudioEventEmitter>();
        foreach (StudioEventEmitter emmiter in emmiters)
        {
            if (emmiter.gameObject.name == "DamageEventEmitter")
            {
                //emmiter.Play(); !!!!
            }
        }
    }

    private void Update()
    {
        if (Player.LookingAtThis(gameObject, KeyCode.E) && !isFixed)
        {
            if (Mathf.Round(_timeHeld) >= ValueStorage.TIME_FIXABLE_TIMETOFIX)
            {
                FixThis();
                FixTrigger?.Invoke();
                _timeHeld = 0f;
            }
            else if (_timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX)
            {
                _timeHeld += Time.deltaTime;
            }
        }
        CheckHolding();
    }

    // submethods //

    private float _timeHeld = 0f;
    private void CheckHolding()
    {
        if (!Input.GetKey(KeyCode.E)) // if the player releases the button [E] then 
        {
            _timeHeld = 0f;
        }
    }

    // built in //

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}