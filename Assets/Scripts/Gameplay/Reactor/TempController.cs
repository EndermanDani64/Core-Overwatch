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
    /// Modifies the temp with all the other influential values
    /// </summary>
    public void TemperatureLoop()
    {
        UpdateTemperatureDynamics();
        if (temp > ValueStorage.REACTOR_TMP_MELTINGPOINT)
        {
            isMeltdown = true;
        }

        if (tempIntensity - coolantInjectionSlider.value / 4 > 0 && SupplyDeposit.supplyedValue > 0)
        {
            tempIntensity -= coolantInjectionSlider.value / 3.5f;
            supplyDeposit.DecreaseSupplyValue(((int)Mathf.Round(coolantInjectionSlider.value)) /*/ 4*/);
        }
        else
        {
            tempIntensity = 0;
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
        else
        {
            simulatedIncrease = 0f;
        }

        temp += simulatedIncrease;
        MathF.Round(temp, 2);

        if (temp < ValueStorage.REACTOR_TMP_MAXIMUM)
        {
            tempTextUpdater.UpdateText();
        }

        if (temp < ValueStorage.REACTOR_TMP_MINIMUM)
        {
            temp = 0;
        }
    }

    private void UpdateTemperatureDynamics()
    {
        difference = temp - previousTemp;
        float delta = difference - previousDifference;

        foreach (var modifier in _intensityModifiers)
        {
            // if the tempIntensity is rising
            if (delta >= modifier.minChange && delta < modifier.maxChange) 
            {
                float[] controlRodValues = new float[] { ControlRod1.value, ControlRod2.value, ControlRod3.value, ControlRod4.value };
                int randomChoice = UnityEngine.Random.Range(0, controlRodValues.Length); // needed for the random choosing of one of the control rod's value

                if (tempIntensity + (modifier.intensityDelta + controlRodValues[randomChoice]) / 5.5f <= 0) // we check so the tempIntensity doesn't go below 0
                {
                    tempIntensity = 0;
                }
                else
                {
                    tempIntensity += (modifier.intensityDelta + controlRodValues[randomChoice]) / 5.5f;
                }

                break;
            }

            // if the tempIntensity is falling
            else if (delta < modifier.minChange) 
            {
                if (tempIntensity - modifier.intensityDelta < ValueStorage.REACTOR_TMP_MINIMUM) // if the tempIntensity would go under 0 then we round it up to 0
                {
                    tempIntensity = ValueStorage.REACTOR_TMP_MINIMUM;
                }
                else // else, we are going to decrease the tempIntensity, with the rod
                {
                    //tempIntensity -= modifier.intensityDelta;
                }
                break;
            }
        }

        //Debug.Log($"difference = {difference}");

        previousDifference = difference;
        previousTemp = temp;
    }
    /// <summary>
    /// Starts the reactor, modifying the isOnline and tempIntensity values
    /// </summary>
    public void StartReactor()
    {
        //Debug.Log("Reactor startup attempt started");
        if (!isOnline)
        {
            isOnline = true;
            tempIntensity = 0.2f;
            previousTemp = temp;
        }
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
            temp = ValueStorage.REACTOR_TMP_MINIMUM;
            tempTextUpdater.UpdateText();
            previousTemp = temp;
            tempIntensity = 0f;
            ControlRod1.value = 0;
        }
    }

    public void DestroyReactor()
    {
        isOnline = false;
        isError = true;
        StopAllCoroutines();
    }

    [Header("Important scripts")]
    [SerializeField] private SoundSystem SoundSystem;
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
    public bool isMeltdown = false;

    public int reactorStatus = 0;

    [SerializeField] private List<TempIntensityModifier> _intensityModifiers;
    public static float temp;
    public static float tempIntensity = 0.2f;

    private float previousTemp;
    private float difference;
    private float previousDifference;
}
