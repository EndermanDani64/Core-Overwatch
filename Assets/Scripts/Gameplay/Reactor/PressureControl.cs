using UnityEngine;
using UnityEngine.UI;
using System;

public class PressureControl : MonoBehaviour
{
    public static float PressureChangeSpeed;

    private int _pressurizeCounter; // - check this please

    public void ChangePressure()
    {
        if (!ReactorManager.ReactorData.IsOnline) 
        {
            ReactorManager.ReactorData.Pressure = 0f;
            return;
        }

        SetMinimumPressure();

        if (ReactorManager.ReactorData.Pressure < ValueStorage.REACTOR_PS_MAX)
        {
            float pressureIncrease = 0;

            float targetPressure = ReactorManager.ReactorData.Temperature / 5f;

            float coolantEffect = coolantInjector.value * 1.0f; // coolant folyadék bejuttatása alapján a nyomás csökkentése

            float valveFactor = Mathf.Lerp(-1f, 1f, ValveOverwatch.valveAmountOpen / 8f); // nyomás csökkentés nyitott szelepek alapján

            targetPressure = (targetPressure - coolantEffect) * valveFactor;

            /* 
                0 szelep = 1.0 (teljes nyomás növekedés),
                5 szelep = -1 (70% csökkentő hatás)
            */

            float pressureChange = Mathf.MoveTowards(ReactorManager.ReactorData.Pressure, targetPressure, PressureChangeSpeed * Time.deltaTime);

            //pressureIncrease = Mathf.Lerp(0, pressureChange, Time.deltaTime * 3f) * 20; // "sima" változás
            pressureIncrease = MathF.Round(pressureIncrease, 2);


            if (ReactorManager.ReactorData.Pressure > ValueStorage.REACTOR_PS_PRESSURIZED && !ReactorManager.ReactorData.IsPressurized)
            {
                ReactorManager.ReactorData.IsPressurized = true;
                overallEvents.PlayEvent("overflowWait");
                Debug.Log("Triggered the overflowWait from pressurecontrol.");
            }

            if (ReactorManager.ReactorData.Pressure + pressureIncrease <= ReactorManager.ReactorData.MinimumPressure)
            {
                if (_pressurizeCounter >= ValueStorage.REACTOR_PS_TOGGLEFIXABLESTRESHOLD && fixables) // 50 - EZ ITT
                {

                    fixables.DamageRandomFixable();
                }
                Debug.Log($"pressurizeCounter = {_pressurizeCounter}");

                ReactorManager.ReactorData.Pressure = ReactorManager.ReactorData.MinimumPressure;
                _pressurizeCounter++;
                WarningManager.ActivateWarningLights("minPs");
            }
            else
            {
                ReactorManager.ReactorData.Pressure += pressureIncrease * GameTimeManager.deltaTime;
                WarningManager.DeactivateWarningLights("minPs");
            }

            //Debug.Log($"Temp={TempController.temp:F0}°C | Valves={ValveOverwatch.valveAmountOpen} | ΔP={pressureIncrease:F2} | P={pressure:F1}");

            //Debug.Log("---------------");
            //Debug.Log($"basePressureChange = {basePressureChange}");
            //Debug.Log($"pressureIncrease = {pressureIncrease}");
        }
    }

    private void SetMinimumPressure()
    {
        ReactorManager.ReactorData.MinimumPressure = ReactorManager.ReactorData.Temperature / ValueStorage.REACTOR_PS_MAX * 100;
        ReactorManager.ReactorData.MinimumPressure = MathF.Round(ReactorManager.ReactorData.MinimumPressure, 3);
        if (ReactorManager.ReactorData.MinimumPressure < 0) ReactorManager.ReactorData.MinimumPressure = 0;

        Debug.Log($"minimum pressure = {ReactorManager.ReactorData.MinimumPressure}");
    }

    // -- Built-in

    private void Start()
    {
        TempController.OnTemperatureChanged += (float _) => { ChangePressure(); };
    }

    // -- references -- //

    [SerializeField] private ButtonManager startupButton;
    [SerializeField] private Fixables fixables;
    [SerializeField] private OverallEvents overallEvents; 

    [SerializeField] private Slider coolantInjector;
}
