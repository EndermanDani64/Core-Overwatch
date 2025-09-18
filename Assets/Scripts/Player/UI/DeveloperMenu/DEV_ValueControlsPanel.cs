using UnityEngine;
using TMPro;

public class DEV_ValueControlsPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField customTempInput;

    public void DEV_SetTemp()
    {
        if (float.TryParse(customTempInput.text, out float customTemp))
        {
            TempController.temp = customTemp;
        }
        else
        {
            Debug.LogWarning("Érvénytelen szám lett beírva!");
        }
    }

    public void DEV_SetEnergy()
    {
        if (float.TryParse(customTempInput.text, out float customEnergy))
        {
            ElectricityManagger.electricity = customEnergy;
        }
        else
        {
            Debug.LogWarning("Érvénytelen szám lett beírva!");
        }
    }

    public void DEV_SetPressure()
    {
        if (float.TryParse(customTempInput.text, out float customPressure))
        {
            PressureControl.pressure = customPressure;
        }
        else
        {
            Debug.LogWarning("Érvénytelen szám lett beírva!");
        }
    }


    //public void
}
