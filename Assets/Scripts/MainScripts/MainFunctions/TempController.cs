using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class TempController : MonoBehaviour
{
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

    public static float temp;
    public static float tempIntensity = 0.2f;

    private float previousTemp;
    private float difference;
    private float previousDifference;

    [System.Serializable]
    public class TempIntensityModifier
    {
        public float minChange;
        public float maxChange;
        public float intensityDelta;
    }

    [SerializeField] private List<TempIntensityModifier> intensityModifiers;

    public void TemperatureLoop()
    {
        UpdateTemperatureDynamics();
        Debug.Log("ts igan");
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

        /*float simulatedIncrease = 0;

        if (!blackoutEvent.isBlackout)
        {
            simulatedIncrease = (tempIntensity * Time.deltaTime * 100) + Mathf.Clamp((ControlRod1.value + ControlRod2.value + ControlRod3.value + ControlRod4.value) * tempIntensity, 0, Random.Range(50, 75));
            simulatedIncrease -= FanOverwatch.fanAmountOnline * 1.5f;
        }
        else
        {
            simulatedIncrease = (tempIntensity * Time.deltaTime * 100) + Mathf.Clamp((ControlRod1.value + ControlRod2.value + ControlRod3.value + ControlRod4.value) * tempIntensity, 0, Random.Range(0, 30));
            simulatedIncrease -= FanOverwatch.fanAmountOnline * 1.5f;
        }*/

        float baseIncrease = tempIntensity * 50f * Time.deltaTime;
        float controlRodEffect = (ControlRod1.value + ControlRod2.value + ControlRod3.value + ControlRod4.value) * 137f * Time.deltaTime;
        float fanCooling = FanOverwatch.fanAmountOnline * 2f * Time.deltaTime;
        float coolantCooling = coolantInjectionSlider.value * 3f * Time.deltaTime;

        float simulatedIncrease = 0f;

        if (blackoutEvent.isBlackout)
        {
            simulatedIncrease = baseIncrease + controlRodEffect;
        }
        else
        {
            simulatedIncrease = baseIncrease + controlRodEffect - fanCooling - coolantCooling;
        }

        Debug.Log($"controlRodEffect = {controlRodEffect}");
        Debug.Log($"simulatedIncrease = {simulatedIncrease}");

        temp += simulatedIncrease;

        if (temp < ValueStorage.REACTOR_TMP_MAXIMUM)
        {
            tempTextUpdater.UpdateText();
        }
    }

    private void UpdateTemperatureDynamics()
    {
        difference = temp - previousTemp;
        float delta = difference - previousDifference;

        if (previousTemp > temp) // if the temp is INCREASING
        {
            ElectricityManagger.electricity -= difference / 2.5f;
        }

        foreach (var modifier in intensityModifiers)
        {
            if (delta >= modifier.minChange && delta < modifier.maxChange)
            {
                float[] controlRodValues = new float[] { ControlRod1.value, ControlRod2.value, ControlRod3.value, ControlRod4.value };
                int choice = Random.Range(0, controlRodValues.Length);
                tempIntensity += modifier.intensityDelta + controlRodValues[choice] / 5.5f;
                //tempIntensity += modifier.intensityDelta + (ControlRod1.value + ControlRod2.value + ControlRod3.value + ControlRod4.value) / 5.5f;
                break;
            }
            else if (delta <= modifier.minChange && delta < modifier.maxChange)
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

        previousDifference = difference;
        previousTemp = temp;
    }

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
}
