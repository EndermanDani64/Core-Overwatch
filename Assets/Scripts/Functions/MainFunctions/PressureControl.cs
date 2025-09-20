using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PressureControl : MonoBehaviour
{
    public static float pressure;
    public bool isError = false;
    
    [SerializeField] private Button startupButton;
    [SerializeField] private TempController TempController; //temp, isOnline

    public int maxPressure;
    public bool isPressurized = false;
    void Start()
    {
        TempController.GetComponent<TempController>();
        maxPressure = 99999; //Random.Range(250, 280)
        //Debug.Log($"maxPressure: {maxPressure}");
    }

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
                if (pressure < maxPressure || isPressurized)
                {
                    if (TempController.temp < 0)
                    {
                        //pressure = 0;
                        //TempController.isOnline = false;
                        //break;
                        continue;
                    }
                    else
                    {
                        if (TempController.isMeltdown == true || isPressurized)
                        {
                            // Nyomás növekedése hõmérséklet alapján
                            float pressureIncrease = Mathf.Clamp(TempController.temp / 15 + Random.Range(0, 1), 1, 15);

                            // Ha hõmérséklet 150°C felett van, gyorsabb növekedés
                            if (TempController.temp >= 150)
                            {
                                pressureIncrease *= 1.3f; // 130%-kal növekszik a növekedés
                            }

                            if (pressure > 2000)
                            {
                                isError = true;
                                isPressurized = true;
                            }

                            if (!isPressurized)
                            {
                                pressure += pressureIncrease;
                            }
                            else
                            {
                                pressure += pressureIncrease * 1.8f;
                                //Debug.Log("!!!WARNING!!!");
                            }

                            yield return new WaitForSeconds(1);
                        }
                        else
                        {
                            pressure += Mathf.Clamp(Random.Range(1, 3), 1, 5);
                            yield return new WaitForSeconds(2);
                        }

                        // Nyomás fokozatos csökkenése (szimulálja a rendszer hõleadását)
                        //if (pressure > 20 && Random.value < 0.2f)
                        //{
                            //pressure -= Random.Range(1, 3);
                        //}
                    }
                }
                else
                {
                    isError = true;
                    isPressurized = true;
                    //DestroyReactor();
                    //break;
                }
            }
            else
            {
                break;
            }
        }
    }

}
