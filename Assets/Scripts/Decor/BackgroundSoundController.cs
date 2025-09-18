using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        source.loop = true;
        source.PlayOneShot(clip);
    }
}
