using UnityEngine;
using System.Collections.Generic;
using System;

public class TempController : MonoBehaviour
{
    [System.Serializable]
    [SerializeField] private class TempIntensityModifier
    {
        public float minChange;
        public float maxChange;
        public float intensityDelta;
    }

    /// <summary>
    /// Used to modify temp. Contains checks for the temp value.
    /// </summary>
    public void TemperatureLoop()
    {
        UpdateTemperatureDynamics();

        if (temp > ValueStorage.REACTOR_TMP_MELTINGPOINT)
        {
            OverallEvents.PlayEvent("meltdown");
        }

        float baseIncrease = tempIntensity * 50f * Time.deltaTime;
        float controlRodEffect = (ControlRod1.value + ControlRod2.value + ControlRod3.value + ControlRod4.value) * 137f * Time.deltaTime;
        float fanCooling = FanOverwatch.fanAmountOnline * 2f * Time.deltaTime;
        float coolantCooling = coolantInjectionSlider.value * 3f * Time.deltaTime;

        float simulatedIncrease = 0f;

        if (OverallEvents.IsBlackout)
        {
            simulatedIncrease = baseIncrease + controlRodEffect;
        }
        else if (!OverallEvents.IsBlackout)
        {
            simulatedIncrease = baseIncrease + controlRodEffect - fanCooling - coolantCooling;
        }

        temp += simulatedIncrease;
        MathF.Round(temp, 2);

        if (temp < ValueStorage.REACTOR_TMP_MAX) tempTextUpdater.UpdateText();
        if (temp < ValueStorage.REACTOR_TMP_MIN) temp = 0;

        OnTemperatureChanged?.Invoke(temp);
    }

    private void UpdateTemperatureDynamics()
    {
        difference = temp - previousTemp;
        float delta = difference - previousDifference;
        bool isDecreasing;
        bool isIncreasing;

        foreach (var modifier in intensityModifiers)
        {
            isDecreasing = delta < modifier.minChange;
            isIncreasing = delta >= modifier.minChange && delta < modifier.maxChange;

            if (isIncreasing && !isDecreasing) 
            {
                float[] controlRodValues = new float[] { ControlRod1.value, ControlRod2.value, ControlRod3.value, ControlRod4.value };
                int randomChoice = UnityEngine.Random.Range(0, controlRodValues.Length);

                tempIntensity += (modifier.intensityDelta + controlRodValues[randomChoice]) / 5.5f;
                Mathf.Max(tempIntensity, 0f);

                OnTemperatureIntensityChanged?.Invoke(tempIntensity);
                break;
            }
            else if (isDecreasing && !isIncreasing)
            {
                Mathf.Max(tempIntensity, 0f);

                OnTemperatureIntensityChanged?.Invoke(tempIntensity);
                break;
            }
        }

        // decreasing tempIntensity based on the coolantInjector
        if (SupplyDeposit.supplyedValue > 0)
        {
            tempIntensity -= coolantInjectionSlider.value / 3.5f;
            Mathf.Max(tempIntensity, 0f);

            supplyDeposit.DecreaseSupplyValue(((int)Mathf.Round(coolantInjectionSlider.value)) /*/ 4*/);
        }

        previousDifference = difference;
        previousTemp = temp;
    }

    public void Transfer_StartReactor(float tempIntensity)
    {
        Debug.Log("Reactor startup by Transfer_StartReactor()");
        if (!isOnline)
        {
            isOnline = true;
            tempIntensity = 0.2f;
            previousTemp = temp;
        }
        else
        {
            Debug.Log("The reactor is already online. The Transfer_StartReactor() function is usless. :/");
        }
    }

    public void Transfer_ForceReset()
    {
        if (isOnline)
        {
            StopAllCoroutines();
            isOnline = false;
            temp = ValueStorage.REACTOR_TMP_MIN;
            tempTextUpdater.UpdateText();
            previousTemp = temp;
            tempIntensity = 0f;
            ControlRod1.value = 0;
        }
    }

    public void StartReactor()
    {
        if (!isOnline)
        {
            isOnline = true;
            tempIntensity = 0.2f;
            previousTemp = temp;
            InvokeRepeating("TemperatureLoop", 0, 2f);
        }
    }

    public void DestroyReactor()
    {
        isOnline = false;
        isError = true;
        StopAllCoroutines();
    }

    // ----  Submethods  ---- //


    // ----  Initialize  ---- //

    [Header("Important scripts")]
    [SerializeField] private PlayerAudioEmitter SoundSystem;
    [SerializeField] private PressureControl pressureControl;
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private BlackoutEvent blackoutEvent;
    //[SerializeField] private ElectricityManagger ElectricityManagger;
    [SerializeField] private SupplyDeposit supplyDeposit;

    [Header("UI")]
    [SerializeField] private UnityEngine.UI.Slider ControlRod1;
    [SerializeField] private UnityEngine.UI.Slider ControlRod2;
    [SerializeField] private UnityEngine.UI.Slider ControlRod3;
    [SerializeField] private UnityEngine.UI.Slider ControlRod4;
    [SerializeField] private UnityEngine.UI.Button startupButton;
    [SerializeField] private TempTextUpdater tempTextUpdater;
    [SerializeField] private UnityEngine.UI.Slider coolantInjectionSlider;


    [Header("Important variables")]
    public bool isOnline = false;
    public bool isError = false;

    public int reactorStatus = 0;

    [SerializeField] private List<TempIntensityModifier> intensityModifiers;
    public static float temp;
    public static float tempIntensity = 0.2f;

    public event Action<float> OnTemperatureChanged;
    public event Action<float> OnTemperatureIntensityChanged;

    private float previousTemp;
    private float difference;
    private float previousDifference;
}
