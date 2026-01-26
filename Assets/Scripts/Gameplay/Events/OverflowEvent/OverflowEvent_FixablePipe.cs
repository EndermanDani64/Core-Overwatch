using System.Collections;
using UnityEngine;

public class OverflowEvent_FixablePipe : MonoBehaviour
{
    public bool isFixed = true;
    [SerializeField] private AudioSource source;
    public AudioClip fixingSFX; 
    public AudioClip burstSFX; 
    public AudioClip leakingSFX; 
    
    /// <summary>
    /// Fixes the pipe only if it's damaged.
    /// </summary>
    public void FixPipe()
    {
        if (!isFixed)
        {
            isFixed = true;
            source.PlayOneShot(fixingSFX);
            gameObject.GetComponent<ParticleSystem>().Stop();
            StartCoroutine(SoftVolumeChangeDown());
        }
        else
        {
            Debug.LogWarning("Cannot fix the pipe because it's already fixed.");
        }
    }

    /// <summary>
    /// Used for turning the looping AudioClip's volume down to 0f.
    /// </summary>
    private IEnumerator SoftVolumeChangeDown()
    {
        while (source.volume > 0f)
        {
            source.volume = Mathf.MoveTowards(source.volume, 0, 1.5f * Time.deltaTime);
            yield return null;
        }
        source.Stop();
        source.volume = 1f; 
    }

    /// <summary>
    /// Used for turn up the looping AudioClip's volume to 1f.
    /// </summary>
    private IEnumerator SoftVolumeChangeUp()
    {
        source.clip = leakingSFX;
        source.volume = 0f;
        source.Play();
        
        while (source.volume < 1f)
        {
            source.volume = Mathf.MoveTowards(source.volume, 0.4f, 1.5f * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// Damages the pipe only if it's fixed.
    /// </summary>
    public void DamagePipe()
    {
        if (isFixed)
        {
            isFixed = false;
            gameObject.GetComponent<ParticleSystem>().Play();
            // source.loop = true;
            StartCoroutine(SoftVolumeChangeUp());
        }
        else
        {
            Debug.LogWarning("Cannot damage the pipe because it's already damaged.");
        }
    }
}
