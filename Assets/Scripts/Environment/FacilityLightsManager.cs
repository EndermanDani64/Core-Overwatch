using UnityEngine;

public class FacilityLightsManager : MonoBehaviour
{

    private GameObject[] FacilityLights = { };

    

    public void LightOutage()
    {
        foreach (GameObject light in FacilityLights)
        {
            Animator lightAnimator = light.GetComponent<Animator>();
            lightAnimator.Play("LightFadeOut");
        }
        playerAudioEmitter.PlaySound("fx_blackout");
    }


    public void LightRestore()
    {
        foreach (GameObject light in FacilityLights)
        {
            Animator lightAnimator = light.GetComponent<Animator>();
            lightAnimator.Play("LightFadeIn");
        }
        //playerAudioEmitter.PlaySound("fx_blackout");
    }

    // -- Built-in -- //

    private void Start()
    {
        EnergyManagger.MasterBatteryData.EnergyConsumption += ValueStorage.ENERGY_CONSUMPTION_LIGHT;

        FacilityLights = GameObject.FindGameObjectsWithTag("CeilingLight");
    }


    [SerializeField] private PlayerAudioEmitter playerAudioEmitter;
}
