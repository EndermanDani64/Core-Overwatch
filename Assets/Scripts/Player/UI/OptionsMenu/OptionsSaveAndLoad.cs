using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsSaveAndLoad : MonoBehaviour
{
    [Header("Important Assets")]
    [SerializeField] private AudioSource audioSource;
    [Header("Values")]
    [SerializeField] private Slider VolumeSlider;

    private void Start()
    {
        
    }
    public void SaveOptionsData()
    {
        audioSource.volume = VolumeSlider.value;
        PlayerPrefs.SetFloat("Volume", VolumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log($"Saved Options, volume: {VolumeSlider.value}");
    }
}
