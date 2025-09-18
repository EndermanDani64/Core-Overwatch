using Unity.VisualScripting;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] private BlackoutEvent Event_Blackout;
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private ElectricityManagger Managger_Electricity;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip soundEffect;

    private float defaultAmbientIntensity;
    private float reflectionIntensity;

    private void Start()
    {
        defaultAmbientIntensity = RenderSettings.ambientIntensity;
        reflectionIntensity = RenderSettings.reflectionIntensity;
    }

    public void LightOutage()
    {
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
        soundSource.PlayOneShot(soundEffect);
    }

    public void LightRestore()
    {
        RenderSettings.ambientIntensity = defaultAmbientIntensity;
        RenderSettings.reflectionIntensity = reflectionIntensity;
        soundSource.PlayOneShot(soundEffect);
    }
}
