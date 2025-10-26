using UnityEngine;

public class OverflowEvent_FixablePipe : MonoBehaviour
{
    public bool isFixed = true;
    [SerializeField] private AudioSource source;
    public AudioClip fixingSFX;
    
    /// <summary>
    /// Fixes the pipe only if it's damaged.
    /// </summary>
    public void FixPipe()
    {
        if (!isFixed)
        {
            isFixed = true;
            source.PlayOneShot(fixingSFX);
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
            isFixed = false;
        }
        else
        {
            Debug.LogWarning("Cannot damage the pipe because it's already damaged.");
        }
    }

    /// <summary>
    /// Plays the fix sound.
    /// </summary>
    public void Sound()
    {
        source.PlayOneShot(fixingSFX);
    }
}
