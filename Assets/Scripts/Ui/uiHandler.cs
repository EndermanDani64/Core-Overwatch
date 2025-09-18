using UnityEngine;
using TMPro;

public class uiHandler : MonoBehaviour
{
    [SerializeField] private TempController tempController;
    [SerializeField] private PressureControl pressureControl;

    [SerializeField] private TMP_Text tempText;
    [SerializeField] private TMP_Text tempText1;

    [SerializeField] private TMP_Text pressureText;
    [SerializeField] private TMP_Text pressureText2;
    //[SerializeField] private TMP_Text pressureText3;
    //[SerializeField] private TMP_Text pressureText4;
    //[SerializeField] private TMP_Text pressureText5;

    [SerializeField] private TMP_Text isOnlineText;
    [SerializeField] private TMP_Text isOnlineText1;

    void Update()
    {
        if (tempController.isOnline == true)
        {
            isOnlineText.text = "Online";
            isOnlineText1.text = "Online";

            // temperature
            tempText.text = $"temp: {Mathf.Round(TempController.temp)}"; //tempController.temp
            tempText1.text = $"temp: {Mathf.Round(TempController.temp)}";
            if (tempController.isError == true)
            {
                tempText.text = "temp: ERROR";
                tempText1.text = "temp: ERROR";
            }

            // pressure
            pressureText.text = $"ps: {PressureControl.pressure}";
            pressureText2.text = $"ps: {PressureControl.pressure}";
            if (pressureControl.isError == true)
            {
                pressureText.text = "ps: ERROR";
            }
        }
        else
        {
            isOnlineText.text = "Offline";
            isOnlineText1.text = "Offline";
        }
    }
}
