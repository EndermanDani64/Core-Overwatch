using UnityEngine;
using UnityEngine.Rendering;

public class LightSystem : MonoBehaviour
{
    /*
       <------------------------------------------------...------------------------------------------------>
                                  This is for the rotating light around the map.
       <------------------------------------------------...------------------------------------------------>
     */

    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;

    [SerializeField] private TempController tempController;
    [SerializeField] private BlackoutEvent blackoutEvent;
    [SerializeField] private OverflowEvent overflowEvent;

    private void Start()
    {
        Light light = GetComponent<Light>();
        light.enabled = false;
        GetComponent<LensFlareComponentSRP>().intensity = 0;

        tempController.OnTemperatureChanged += MeltdownRotatingLightCheck;
        blackoutEvent.BlackoutEvent_SatusChange += BlackoutRotatingLightCheck;
        overflowEvent.OverflowEvent_SatusChange += OverflowRotatingLightCheck;
    }

    private void MeltdownRotatingLightCheck(float temp)
    {
        if (temp > ValueStorage.REACTOR_TMP_MELTINGPOINT)
        {
            GetComponent<Light>().color = Color.red;
            GetComponent<Light>().enabled = true;
            GetComponent<LensFlareComponentSRP>().intensity = 1;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
    }

    private void BlackoutRotatingLightCheck(bool isBlackout)
    {
        if (isBlackout)
        {
            GetComponent<Light>().color = Color.yellow;
            GetComponent<Light>().enabled = true;
            GetComponent<LensFlareComponentSRP>().intensity = 1;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<Light>().enabled = false;
            GetComponent<LensFlareComponentSRP>().intensity = 0;
        }
    }

    private void OverflowRotatingLightCheck(bool isOverflow)
    {
        if (isOverflow)
        {
            GetComponent<Light>().color = Color.yellow;
            GetComponent<Light>().enabled = true;
            GetComponent<LensFlareComponentSRP>().intensity = 1;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<Light>().enabled = false;
            GetComponent<LensFlareComponentSRP>().intensity = 0;
        }
    }
}
