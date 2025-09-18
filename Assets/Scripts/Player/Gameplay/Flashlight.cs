using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashLight;

    private bool isFlashlightActive = true;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            isFlashlightActive = !isFlashlightActive; 

            if (isFlashlightActive)
            {
                flashLight.intensity = 65.52f;
            }
            else
            {
                flashLight.intensity = 0f;
            }
        }
    }
}
