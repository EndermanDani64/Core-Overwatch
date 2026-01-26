using UnityEngine;
using TMPro;

public class TempTextUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text text2;

    public void UpdateText()
    {
        if (TempController.temp < ValueStorage.REACTOR_TMP_MAXIMUM)
        {
            text.text = $"temp: {Mathf.Round(TempController.temp)}";
            text2.text = $"tempintensity: {TempController.tempIntensity}";
        }
        else
        {
            text.text = "temp: ERROR";
            text2.text = $"tempintensity: {TempController.tempIntensity}";
        }
    }
}
