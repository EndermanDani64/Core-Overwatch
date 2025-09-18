using UnityEngine;

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
    }

    void Update()
    {
        if (TempController.isMeltdown == true)
        {
            GetComponent<Light>().color = Color.red;
            GetComponent<Light>().enabled = true;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
        else if (BlackoutEvent.isBlackout == true)
        {
            GetComponent<Light>().color = Color.yellow;
            GetComponent<Light>().enabled = true;
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
        else if (BlackoutEvent.isBlackout == false)
        {
            GetComponent<Light>().enabled = false;
        }
    }

}
