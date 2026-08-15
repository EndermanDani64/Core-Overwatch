using UnityEngine;
using System.Collections.Generic;
using System;

public class TempController : MonoBehaviour
{
    [System.Serializable]
    public class TempIntensityModifier
    {
        public float minChange;
        public float maxChange;
        public float intensityDelta;
    }


    /// <summary>
    /// Used to modify temp. Contains checks for the temp value.
    /// </summary>
    public void ChangeTemperature(float tickDeltaTime)
    {
        UpdateTemperatureDynamics();

        if (ReactorManager.ReactorData.Temperature > ValueStorage.REACTOR_TMP_MELTINGPOINT)
        {
            OverallEvents.PlayEvent("meltdown_first");
        }

        float baseIncrease = TempIntensity * 40f * Time.deltaTime;
        float controlRodEffect = (ControlRod1.Value + ControlRod2.Value + ControlRod3.Value + ControlRod4.Value) * 140f * Time.deltaTime;
        float fanCooling = FanOverwatch.OnlineFanCount * 2f * Time.deltaTime;
        float coolantCooling = coolantInjectionSlider.value * 3f * Time.deltaTime;

        float targetTemperatureChange = 0f;

        if (OverallEvents.IsBlackout)
        {
            targetTemperatureChange = baseIncrease + controlRodEffect;
        }
        else if (!OverallEvents.IsBlackout)
        {
            targetTemperatureChange = baseIncrease + controlRodEffect - fanCooling - coolantCooling;
        }

        targetTemperatureChange *= tickDeltaTime;

        ReactorManager.ReactorData.Temperature += targetTemperatureChange;



        ReactorManager.ReactorData.Temperature = MathF.Round(ReactorManager.ReactorData.Temperature, 2);

        if (ReactorManager.ReactorData.Temperature < ValueStorage.REACTOR_TMP_MIN) ReactorManager.ReactorData.Temperature = 0;

        OnTemperatureChanged?.Invoke(ReactorManager.ReactorData.Temperature);
    }


    private void UpdateTemperatureDynamics()
    {
        difference = ReactorManager.ReactorData.Temperature - previousTemp;
        float delta = difference - previousDifference;
        bool isDecreasing;
        bool isIncreasing;

        foreach (var modifier in intensityModifiers)
        {
            isDecreasing = delta < modifier.minChange;
            isIncreasing = delta >= modifier.minChange && delta < modifier.maxChange;

            if (isIncreasing && !isDecreasing) 
            {
                float[] controlRodValues = new float[] { ControlRod1.Value, ControlRod2.Value, ControlRod3.Value, ControlRod4.Value };
                int randomChoice = UnityEngine.Random.Range(0, controlRodValues.Length);

                TempIntensity += (modifier.intensityDelta + controlRodValues[randomChoice]) / 5.5f;
                Mathf.Max(TempIntensity, 0f);

                OnTemperatureIntensityChanged?.Invoke(TempIntensity);
                break;
            }
            else if (isDecreasing && !isIncreasing)
            {
                Mathf.Max(TempIntensity, 0f);

                OnTemperatureIntensityChanged?.Invoke(TempIntensity);
                break;
            }
        }

        // decreasing tempIntensity based on the coolantInjector
        if (SupplyDeposit.supplyedValue > 0)
        {
            TempIntensity -= coolantInjectionSlider.value / 3.5f;
            Mathf.Max(TempIntensity, 0f);

            supplyDeposit.DecreaseSupplyValue(((int)Mathf.Round(coolantInjectionSlider.value)) /*/ 4*/);
        }

        previousDifference = difference;
        previousTemp = ReactorManager.ReactorData.Temperature;
    }


    /// <summary>
    /// Mostly used at the starting of the reactor.
    /// </summary>
    public void SetToDeafultValues()
    {
        TempIntensity = 0.2f;
        previousTemp = ReactorManager.ReactorData.Temperature;
    }


    /*public void Transfer_StartReactor(float tempIntensity)
    {
        Debug.Log("Reactor startup by Transfer_StartReactor()");
        if (!reactorManager.IsOnline)
        {
            reactorManager.IsOnline = true;
            tempIntensity = 0.2f;
            previousTemp = reactorManager.Temp;
        }
        else
        {
            Debug.Log("The reactor is already online. The Transfer_StartReactor() function is usless. :/");
        }
    }*/


    /*public void Transfer_ForceReset()
    {
        if (isOnline)
        {
            StopAllCoroutines();
            isOnline = false;
            reactorManager.Temp = ValueStorage.REACTOR_TMP_MIN;
            tempTextUpdater.UpdateText();
            previousTemp = reactorManager.Temp;
            tempIntensity = 0f;
            ControlRod1.value = 0;
        }
    }*/



    // ----  Built-in  ---- //


    private void Start()
    {
        ReactorManager.OnReactorStart += SetToDeafultValues;
        ReactorManager.OnReactorStart += () => { GameTimeManager.OnTickUpdate += ChangeTemperature; };
    }


    // ----  Initialize  ---- //

    [Header("Important scripts")]
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private BlackoutEvent blackoutEvent;
    [SerializeField] private SupplyDeposit supplyDeposit;

    [Header("UI")]
    [SerializeField] private SliderManager ControlRod1;
    [SerializeField] private SliderManager ControlRod2;
    [SerializeField] private SliderManager ControlRod3;
    [SerializeField] private SliderManager ControlRod4;
    [SerializeField] private UnityEngine.UI.Slider coolantInjectionSlider;

    [SerializeField] private List<TempIntensityModifier> intensityModifiers;
    public static float TempIntensity = 0.2f;

    public static event Action<float> OnTemperatureChanged;
    public static event Action<float> OnTemperatureIntensityChanged;

    private float previousTemp;
    private float difference;
    private float previousDifference;
}
