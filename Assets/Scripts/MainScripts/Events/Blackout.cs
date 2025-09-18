using System.Collections;
using UnityEngine;

public class BlackoutEvent : MonoBehaviour
{
    private float defaultAmbientIntensity;
    private float reflectionIntensity;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioClip music;

    //[SerializeField] private Light flashLight;

    [SerializeField] private TempController tempController;
    [SerializeField] private ElectricityManagger electricityManagger;
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private ShoutSystem ShoutSystem;

    public bool isBlackout = false;

    void Start()
    {
        //flashLight.enabled = false;
        defaultAmbientIntensity = RenderSettings.ambientIntensity;
        reflectionIntensity = RenderSettings.reflectionIntensity;
        StartCoroutine(randomEvent());
    }
    public IEnumerator SetPower() // this is called when the randomEvent() has got the randomness
    {   
        Debug.Log("Blackout Event has Started!");
        OverallEvents.IsEventRunning = true;
        StopRandomEventCheck();
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
        source.PlayOneShot(soundEffect);
        if (!tempController.isMeltdown)
        {
            source.PlayOneShot(music);
        }
        isBlackout = true;
        //flashLight.enabled = true;

        ShoutSystem.ShowMessage("Power went, whoossss-");
        yield return new WaitForSeconds(123);
        RenderSettings.ambientIntensity = defaultAmbientIntensity;
        RenderSettings.reflectionIntensity = reflectionIntensity;
        isBlackout = false;
        ShoutSystem.HideMessage();
        StartCoroutine(randomEvent());
        //flashLight.enabled = false;
    }

    private bool doesRandomHaveToStop = false;
    
    private IEnumerator randomEvent()
    {
        Debug.Log("Random blackouts will occour again.");
        while (!doesRandomHaveToStop)
        {
            int willEventStart = Random.Range(0, 150);
            if (willEventStart == 69 && !isBlackout && !tempController.isMeltdown && !electricityManagger.isDepletedEnergy)
            {
                StartCoroutine(SetPower());
            }
            yield return new WaitForSeconds(2);
        }
    }

    public void ForceBlackout()
    {
        StartCoroutine(SetPower());
    }
    public bool DEV_ForceBlackout()
    {
        StartCoroutine(SetPower());
        return true;
    }

    public void ForceStop()
    {   
        RenderSettings.ambientIntensity = defaultAmbientIntensity;
        RenderSettings.reflectionIntensity = reflectionIntensity;
        //source.volume = 0.6f;
        isBlackout = false;
        //source.PlayOneShot(soundEffect);
    }

    public void ForceRestoreLights()
    {
        RenderSettings.ambientIntensity = defaultAmbientIntensity;
        RenderSettings.reflectionIntensity = reflectionIntensity;
        //source.volume = 0.6f;
        //source.PlayOneShot(soundEffect);
    }

    public void ForceLightsOut()
    {
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
        source.PlayOneShot(soundEffect);
    }

    public void StopRandomEventCheck()
    {
        doesRandomHaveToStop = true;
        Debug.Log("Random blackouts won't occour again.");
    }
}
