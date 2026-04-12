using FMODUnity;
using System.Collections;
using UnityEngine;

public class OverflowEvent_FixablePipe : MonoBehaviour
{
    public bool isFixed = true;
    [SerializeField] private StudioEventEmitter _fixingEmmiter;
    [SerializeField] private StudioEventEmitter _leakingEmmiter;
    [SerializeField] private EventReference _fx_fixing;
    [SerializeField] private EventReference _fx_leakingLiquid;

    /// <summary>
    /// Fixes the pipe only if it's damaged.
    /// </summary>
    public void FixPipe()
    {
        if (!isFixed)
        {
            _leakingEmmiter.Stop();
            _fixingEmmiter.Play();
            isFixed = true;
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            Debug.LogWarning("Cannot fix the pipe because it's already fixed.");
        }
    }

    /// <summary>
    /// Damages the pipe only if it's fixed.
    /// </summary>
    public void DamagePipe()
    {
        if (isFixed)
        {
            _leakingEmmiter.Play();
            _fixingEmmiter.Stop();
            isFixed = false;
            gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            Debug.LogWarning("Cannot damage the pipe because it's already damaged.");
        }
    }
}
