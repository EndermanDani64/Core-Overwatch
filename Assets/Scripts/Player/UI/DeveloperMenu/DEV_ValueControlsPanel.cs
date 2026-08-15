using UnityEngine;
using TMPro;

public class DEV_ValueControlsPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField customTempInput;

    public void DEV_SetTemp()
    {
        if (float.TryParse(customTempInput.text, out float customTemp))
        {
            ReactorManager.ReactorData.Temperature = customTemp;
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
            EnergyManagger.electricity = customEnergy;
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
            ReactorManager.ReactorData.Pressure = customPressure;
        }
        else
        {
            Debug.LogWarning("Érvénytelen szám lett beírva!");
        }
    }


    //public void
}
