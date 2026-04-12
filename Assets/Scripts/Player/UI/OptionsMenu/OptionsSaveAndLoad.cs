using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
public class OptionsSaveAndLoad : MonoBehaviour
{
    [Header("Important Assets")]
    [SerializeField] private PlayerAudioEmitter _playerAudioSource;
    //[SerializeField] private AudioSource atmosphereAudioSource;
    [Header("Values")]
    [SerializeField] private Slider playerAudioVolumeSlider;
    [SerializeField] private Slider atmosphereAudioVolumeSlider;
    public void SaveOptionsData()
    {
        //_playerAudioSource.volume = playerAudioVolumeSlider.value;    
        //atmosphereAudioSource.volume = atmosphereAudioVolumeSlider.value;

        /*PlayerPrefs.SetFloat("PlayerAudioVolume", playerAudioVolumeSlider.value);
        PlayerPrefs.SetFloat("AtmosphereAudioVolume", atmosphereAudioVolumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log($"Saved Options, volume: {playerAudioVolumeSlider.value}");*/
    }
}
