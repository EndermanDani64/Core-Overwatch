using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class FanControl : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private UnityEngine.UI.Button mainButton;
    [SerializeField] private TMP_Text buttonText;

    [Header("Important variables")]
    private bool IsOnline = false;

    void Start()
    {
        buttonText.text = "Offline";
    }

    public void ToggleFanStatus()
    {
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(ButtonPressCooldown());
    }

    public void Transfer_ForceReset()
    {
        if (IsOnline)
        {
            IsOnline = false;

            EnergyManagger.MasterBatteryData.EnergyConsumption -= ValueStorage.ENERGY_CONSUMPTION_FAN;
            buttonText.text = "Offline";
        }
        else
        {
            Debug.LogWarning("The Transfer_ForceReset cannot be called caused by the true IsOnline variable.");
        }
    }

    private IEnumerator ButtonPressCooldown()
    {
        if (!IsOnline)
        {
            mainButton.interactable = false;
            IsOnline = true;
            EnergyManagger.MasterBatteryData.EnergyConsumption += ValueStorage.ENERGY_CONSUMPTION_FAN;
            buttonText.text = "Online";
            yield return new WaitForSeconds(5);
            mainButton.interactable = true;
            yield break;
        }
        else
        {
            mainButton.interactable = false;
            IsOnline = false;
            EnergyManagger.MasterBatteryData.EnergyConsumption -= ValueStorage.ENERGY_CONSUMPTION_FAN;
            buttonText.text = "Offline";
            yield return new WaitForSeconds(5);
            mainButton.interactable = true;
            yield break;
        }
    }
}
