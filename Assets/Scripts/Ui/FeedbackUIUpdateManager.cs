using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FeedbackUIUpdateManager : MonoBehaviour
{
    private List<TMP_Text> TemperatureTexts = new List<TMP_Text>();
    private List<TMP_Text> TempIntensityTexts = new List<TMP_Text>();
    private List<TMP_Text> PressureTexts = new List<TMP_Text>();
    private List<TMP_Text> EnergyTexts = new List<TMP_Text>();

    private void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("UI_TemperatureFeedbackText"))
        {
            TMP_Text text;
            if (gameObject.TryGetComponent<TMP_Text>(out text))
                TemperatureTexts.Add(text);
        }
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("UI_TempIntensityFeedbackText"))
        {
            TMP_Text text;
            if (gameObject.TryGetComponent<TMP_Text>(out text))
                TempIntensityTexts.Add(text);
        }
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("UI_PressureFeedbackText"))
        {
            TMP_Text text;
            if (gameObject.TryGetComponent<TMP_Text>(out text))
                PressureTexts.Add(text);
        }
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("UI_EnergyFeedbackText"))
        {
            TMP_Text text;
            if (gameObject.TryGetComponent<TMP_Text>(out text))
                EnergyTexts.Add(text);
        }

        GameTimeManager.OnTickUpdate += UpdateFeedbackUI;
    }

    private void UpdateFeedbackUI(float _)
    {
        foreach (TMP_Text targetText in TemperatureTexts)
        {
            if (ReactorManager.ReactorData.ReactorStatus == "error") {
                targetText.text = "Temperature: ERROR";
                continue;
            }

            targetText.text = $"Temperature: {Mathf.Round(ReactorManager.ReactorData.Temperature * 10) / 10}";
        }

        foreach (TMP_Text targetText in TempIntensityTexts)
        {
            if (ReactorManager.ReactorData.ReactorStatus == "error") {
                targetText.text = "Tempintensity: ERROR";
                continue;
            }

            targetText.text = $"Tempintensity: {Mathf.Round(TempController.TempIntensity * 100) / 100}";
        }

        foreach (TMP_Text targetText in PressureTexts)
        {
            if (ReactorManager.ReactorData.ReactorStatus == "error")
            {
                targetText.text = "Pressure: ERROR";
                continue;
            }

            targetText.text = $"Pressure: {Mathf.Round(ReactorManager.ReactorData.Pressure * 100) / 100}";
        }

        foreach (TMP_Text targetText in EnergyTexts)
        {
            if (ReactorManager.ReactorData.ReactorStatus == "error")
            {
                targetText.text = "Energy: ERROR";
                continue;
            }

            targetText.text = $"Energy: {Mathf.Round(EnergyManagger.MasterBatteryData.Energy)}";
        }
    }
}
