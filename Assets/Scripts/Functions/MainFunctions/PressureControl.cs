using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PressureControl : MonoBehaviour
{
    public static float pressure;
    public bool isError = false;
    
    [SerializeField] private Button startupButton;
    [SerializeField] private TempController TempController;
    [SerializeField] private Fixables fixables;

    [SerializeField] private Slider coolantInjector;

    public static bool isPressurized = false;

    public void StartupReactor()
    {
        //Debug.Log($"StartupReactor() hívva - isOnline: {TempController.isOnline}");

        if (!TempController.isOnline)
        {
            StopCoroutine(PressureControlFunction());
            pressure = 0;
        }
        else
        {
            Debug.Log("Pressure coroutine indul");
            StartCoroutine(PressureControlFunction());
        }
    }

    public void DestroyReactor()
    {
        Debug.Log("bumm");
        StopAllCoroutines();
        TempController.DestroyReactor();
    }

    private IEnumerator PressureControlFunction()
    {
        while (true)
        {
            if (TempController.isOnline)
            {
                if (pressure < ValueStorage.REACTOR_PS_MAX)
                {
                    if (pressure > ValueStorage.REACTOR_PS_PRESSURIZED)
                    {
                        //float pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(0, 1), 1, 15); !!!!!!
                    }
                    else
                    {
                        float pressureIncrease = 0f;
                        if (FanOverwatch.fanAmountOnline == 5)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-4, 1) - coolantInjector.value * 1.5f, -12, 2);
                        }
                        else if(FanOverwatch.fanAmountOnline == 4)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-3, 2) - coolantInjector.value * 1.5f, -9, 2);
                        }
                        else if (FanOverwatch.fanAmountOnline == 3)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-3, 3) - coolantInjector.value * 1.5f, -7, 3);
                        }
                        else if (FanOverwatch.fanAmountOnline == 2)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-1, 4) - coolantInjector.value * 1.5f, -5, 4);
                        }
                        else if (FanOverwatch.fanAmountOnline == 1)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(0, 5) - coolantInjector.value * 1.5f, -3, 5);
                        }
                        else
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / Random.Range(4, 6) - coolantInjector.value * 1.5f, 0, 8);
                        }
                        pressure += pressureIncrease;
                    }
                }
                else
                {
                    isError = true;
                    isPressurized = true;
                    Debug.LogWarning("!!!PRESSURE MAX REACHED!!!");
                }
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(2.2f);
        }
    }
}
