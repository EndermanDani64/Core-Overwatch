using System;
using System.Collections;
using UnityEngine;

public class ContinousManager : MonoBehaviour
{
    [Header("Scripts that contains the functions")]
    [SerializeField] private SupplyDeposit supplyDeposit;
    [SerializeField] private ValueStorage valueStorage;
    [SerializeField] private ElectricityDecreaseValueManagger electricityDValueManagger;
    [SerializeField] private GeneratorController generatorController;
    [SerializeField] private TempController tempController;
    [SerializeField] private ScoreManager scoreManager;

    [Header("UI")]
    [SerializeField] private UnityEngine.UI.Slider coolantInjectionSlider;

    private void Start()
    {
        StartCoroutine(SecondsTrigger_E05());
        StartCoroutine(SecondsTrigger_E1());
        StartCoroutine(SecondsTrigger_E25());
    }

    public IEnumerator SecondsTrigger_E05() // E1 = every .5 sec
    {
        while (true)
        {
            if (tempController.isOnline)
            {
                tempController.TemperatureLoop();
            }
            valueStorage.UpdateValue("COOLANT_SUPPLY_DECREASE", Convert.ToInt32(coolantInjectionSlider.value));
            yield return new WaitForSeconds(.5f);
        }
    }

    public IEnumerator SecondsTrigger_E1() // E1 = every 1 sec
    {
        while (true)
        {
            electricityDValueManagger.DValueUpdate();
            scoreManager.CheckPossibleScores();
            yield return new WaitForSeconds(1);
            generatorController.ChangeEnergy();
        }
    }

    public IEnumerator SecondsTrigger_E25() // E25 = every 2.5 sec
    {
        while (true)
        {
            if (tempController.isOnline)
            {
                supplyDeposit.DecreaseSupplyValue(ValueStorage.COOLANT_SUPPLY_DECREASE);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }
}
