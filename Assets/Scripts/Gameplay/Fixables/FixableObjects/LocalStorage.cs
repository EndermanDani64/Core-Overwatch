using UnityEngine;
using FMODUnity;
using System;

public class LocalStorage : MonoBehaviour
{
    public delegate void InteractedWith();
    public event InteractedWith FixTrigger;

    public bool isFixed = false;
    public AudioClip fixingSFX;

    public void FixThis()
    {
        if (isFixed) return;

        ParticleSystem.MainModule particleSystem = gameObject.GetComponent<ParticleSystem>().main;

        isFixed = true;
        gameObject.GetComponent<StudioEventEmitter>().Play();
        particleSystem.duration = 0;
    }

    public void DamageThis()
    {
        if (!isFixed) return;

        ParticleSystem.MainModule particleSystem = gameObject.GetComponent<ParticleSystem>().main;

        isFixed = false;
        gameObject.GetComponent<StudioEventEmitter>().Play();
        particleSystem.duration = 0.13f;
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