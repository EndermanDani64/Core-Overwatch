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

    public static bool isPressurized = false;

    private Coroutine increaseRoutine;

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
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-4, 1), -12, 1);
                        }
                        else if(FanOverwatch.fanAmountOnline == 4)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-3, 2), -9, 3);
                        }
                        else if (FanOverwatch.fanAmountOnline == 3)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-3, 3), -7, 6);
                        }
                        else if (FanOverwatch.fanAmountOnline == 2)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(-1, 4), -5, 9);
                        }
                        else if (FanOverwatch.fanAmountOnline == 1)
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(0, 5), -2, 12);
                        }
                        else
                        {
                            pressureIncrease = Mathf.Clamp(TempController.temp / Random.Range(4, 6), 0, 15);
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
        }
    }

}
