using System;
using System.Collections;
using UnityEngine;
public class GameTimeManager : MonoBehaviour
{
    [Header("Script references")]
    [SerializeField] private SupplyDeposit _SupplyDeposit;
    [SerializeField] private ValueStorage _ValueStorage;
    [SerializeField] public ElectricityDecreaseValueManagger _ElectricityDValueManagger;
    [SerializeField] public GeneratorController _GeneratorController;
    [SerializeField] private TempController _TempController;
    [SerializeField] public ScoreManager _ScoreManager;
    [SerializeField] private OverflowEvent _OverflowEvent;
    [SerializeField] private EconomyController _EconomyController;

    [Header("Visuals")]
    [SerializeField] private UnityEngine.UI.Slider coolantInjectionSlider;

    private void Start()
    {
        // StartCoroutine(SecondsTrigger_E05());
        StartCoroutine(SecondsTrigger_E1());
        StartCoroutine(SecondsTrigger_E2());
        StartCoroutine(SecondsTrigger_E25());
        // StartCoroutine(SecondsTrigger_E60());
    }

    /// <summary>
    /// Runs every contained functions in a 0.5 second delay.
    /// </summary>
    public IEnumerator SecondsTrigger_E05() // E1 = every .5 sec
    {
        while (true)
        {
            
            yield return new WaitForSeconds(.5f);
        }
    }

    /// <summary>
    /// Runs every contained functions in a 1 second delay.
    /// </summary>
    public IEnumerator SecondsTrigger_E1()
    {
        while (true)
        {
            _ElectricityDValueManagger.DValueUpdate();
            _ScoreManager.CheckPossibleScores();
            _EconomyController.CheckMoneyAward();
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Runs every contained functions in a 2 second delay.
    /// </summary>
    public IEnumerator SecondsTrigger_E2()
    {
        while (true)
        {
            if (_TempController.isOnline) { _TempController.TemperatureLoop(); }
            _OverflowEvent.UpdateDifficulty();
            _ValueStorage.UpdateValue("COOLANT_SUPPLY_DECREASE", Convert.ToInt32(coolantInjectionSlider.value));
            yield return new WaitForSeconds(2f);
            _GeneratorController.ChangeEnergy();
        }
    }

    /// <summary>
    /// Runs every contained functions in a 2.5 second delay.
    /// </summary>
    public IEnumerator SecondsTrigger_E25()
    {
        while (true)
        {
            if (_TempController.isOnline)
            {
                _SupplyDeposit.DecreaseSupplyValue(ValueStorage.COOLANT_SUPPLY_DECREASE);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }

    int randomInt = 0;
    /// <summary>
    /// Runs every contained functions in a 60 second delay.
    /// </summary>
    public IEnumerator SecondsTrigger_E60()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
        }
    }
}
