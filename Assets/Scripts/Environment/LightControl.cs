using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] private BlackoutEvent Event_Blackout;
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private ElectricityManagger Managger_Electricity;

    private GameObject[] FacilityLights_Main = { };

    private void Start()
    {
        FacilityLights_Main = GameObject.FindGameObjectsWithTag("CeilingLight");
    }

    /// <summary>
    /// Plays the "LightFadeOut" animation clip on every GameObject that has the tag "CeilingLight".
    /// </summary>
    public void LightOutage()
    {
        foreach (GameObject light in FacilityLights_Main)
        {
            Animator lightAnimator = light.GetComponent<Animator>();
            lightAnimator.Play("LightFadeOut");
        }
    }

    /// <summary>
    /// Plays the "LightFadeIn" animation clip on every GameObject that has the tag "CeilingLight".
    /// </summary>
    public void LightRestore()
    {
        foreach (GameObject light in FacilityLights_Main)
        {
            Animator lightAnimator = light.GetComponent<Animator>();
            lightAnimator.Play("LightFadeIn");
        }
    }
}
