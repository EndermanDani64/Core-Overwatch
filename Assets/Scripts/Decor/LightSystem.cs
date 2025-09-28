using UnityEngine;
using UnityEngine.Rendering;

public class LightSystem : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;

    [SerializeField] private TempController TempController;
    [SerializeField] private BlackoutEvent BlackoutEvent;

    private void Start()
    {
        Light light = GetComponent<Light>();
        light.enabled = false;
        GetComponent<LensFlareComponentSRP>().intensity = 0;
    }

    void Update()
    {
        if (TempController.isMeltdown == true)
        {
            GetComponent<Light>().color = Color.red;
            GetComponent<Light>().enabled = true;
            GetComponent<LensFlareComponentSRP>().intensity = 1;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
        else if (BlackoutEvent.isBlackout == true)
        {
            GetComponent<Light>().color = Color.yellow;
            GetComponent<Light>().enabled = true;
            GetComponent<LensFlareComponentSRP>().intensity = 1;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
        else if (BlackoutEvent.isBlackout == false)
        {
            GetComponent<Light>().enabled = false;
            GetComponent<LensFlareComponentSRP>().intensity = 0;
        }
    }

}
