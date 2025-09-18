using Unity.VisualScripting;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private CoreEffects CoreEffects;
    public AudioSource source;
    public AudioClip backgroundSounds;
    //[SerializeField] private TempController MainControl;
    //[SerializeField] private PressureControl PressureControl;

    public void BackgroundSoundsMute()
    {
        source.Stop();
    }
}
