using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PressureControl : MonoBehaviour
{
    public static float pressure;
    public bool isError = false;
    
    [SerializeField] private Button startupButton;
    [SerializeField] private TempController tempController;
    [SerializeField] private OverflowEvent overflowEvent;
    [SerializeField] private Fixables fixables;
    [SerializeField] private OverallEvents overallEvents; 

    [SerializeField] private Slider coolantInjector;

    public static bool isPressurized = false;

    public void StartupReactor()
    {
        if (!tempController.isOnline)
        {
            StopCoroutine(PressureControlFunction());
            pressure = 0;
        }
        else
        {
            StartCoroutine(PressureControlFunction());
        }
    }

    public void DestroyReactor()
    {
        StopAllCoroutines();
        tempController.DestroyReactor();
    }

    private IEnumerator PressureControlFunction()
    {
        while (tempController.isOnline) // ameddig online a reaktor
        {
            if (pressure < ValueStorage.REACTOR_PS_MAX)
            {
                float pressureIncrease = 0;

                float basePressureChange = (TempController.temp / 4000f) * 85f; // alap érték a nyomás növeléséhez temp alapján

                float coolantEffect = coolantInjector.value * 1.0f; // coolant folyadék bejuttatása alapján a nyomás csökkentése

                // float randomness = UnityEngine.Random.Range(-1.5f, 1.5f); // véletlen szórás a rendszer "instabilitására"

                float valveFactor = Mathf.Lerp(1f, -1f, ValveOverwatch.valveAmountOpen / 5f); // nyomás csökkentés nyitott szelepek alapján
                Debug.Log($"valveFactor = {valveFactor}");

                /* 
                    0 szelep = 1.0 (teljes nyomás növekedés),
                    5 szelep = -1 (70% csökkentő hatás)
                */

                float targetPressureChange = (basePressureChange - coolantEffect /*+ randomness*/) * valveFactor;
                pressureIncrease = Mathf.Lerp(0, targetPressureChange, Time.deltaTime * 3f) * 100; // "sima" változás

                MathF.Round(pressureIncrease, 2);
                pressure += pressureIncrease;

                if (pressure > ValueStorage.REACTOR_PS_PRESSURIZED && !isPressurized)
                {
                    isPressurized = true;
                    overallEvents.PlayEvent("overflowWait");
                    Debug.Log("Triggered the overflowWait from pressurecontrol.");
                }

                if (pressure <= 0)
                {
                    pressure = 0;
                }
                else
                {
                    pressure += pressureIncrease;
                    MathF.Round(pressure, 2);
                }

                //Debug.Log($"Temp={TempController.temp:F0}°C | Valves={ValveOverwatch.valveAmountOpen} | ΔP={pressureIncrease:F2} | P={pressure:F1}");

                //Debug.Log("---------------");
                //Debug.Log($"basePressureChange = {basePressureChange}");
                //Debug.Log($"pressureIncrease = {pressureIncrease}");
            }

            yield return new WaitForSeconds(2.2f);
        }
    }
}
