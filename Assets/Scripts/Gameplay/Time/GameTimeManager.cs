using System;
using System.Collections;
using UnityEngine;
public class GameTimeManager : MonoBehaviour
{
    public float GameTime { get; private set; }
    public static float deltaTime = 0f;

    public static event Action<float> OnTickUpdate;

    private float _tickDeltaTime = 0f;
    private float _tickUpdateFrequency = .5f;

    private void Start()
    {
        StartCoroutine(_SecondsTrigger_E1());
        StartCoroutine(_SecondsTrigger_E2());
        StartCoroutine(_SecondsTrigger_E25());
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        _tickDeltaTime += Time.deltaTime;
        deltaTime = _tickDeltaTime;

        if (_tickDeltaTime >= _tickUpdateFrequency)
        {
            OnTickUpdate?.Invoke(_tickDeltaTime);
            _tickDeltaTime = 0f;
        }
    }

    /// <summary>
    /// Runs every contained functions in a 0.5 second delay.
    /// </summary>
    public IEnumerator _SecondsTrigger_E05() // E1 = every .5 sec
    {
        while (true)
        {
            
            yield return new WaitForSeconds(.5f);
        }
    }

    /// <summary>
    /// Runs every contained functions in a 1 second delay.
    /// </summary>
    public IEnumerator _SecondsTrigger_E1()
    {
        while (true)
        {
            _ScoreManager.CheckPossibleScores();
            _EconomyController.CheckMoneyAward();
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Runs every contained functions in a 2 second delay.
    /// </summary>
    public IEnumerator _SecondsTrigger_E2()
    {
        while (true)
        {
            _OverflowEvent.UpdateDifficulty();
            _ValueStorage.UpdateValue("COOLANT_SUPPLY_DECREASE", Convert.ToInt32(coolantInjectionSlider.value));
            yield return new WaitForSeconds(2f);
        }
    }

    /// <summary>
    /// Runs every contained functions in a 2.5 second delay.
    /// </summary>
    public IEnumerator _SecondsTrigger_E25()
    {
        while (true)
        {
            if (ReactorManager.ReactorData.IsOnline)
            {
                _SupplyDeposit.DecreaseSupplyValue(ValueStorage.COOLANT_SUPPLY_DECREASE);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }

    [Header("Script references")]
    [SerializeField] private SupplyDeposit _SupplyDeposit;
    [SerializeField] private ValueStorage _ValueStorage;
    [SerializeField] public ScoreManager _ScoreManager;
    [SerializeField] private OverflowEvent _OverflowEvent;
    [SerializeField] private EconomyController _EconomyController;

    [Header("Visuals")]
    [SerializeField] private UnityEngine.UI.Slider coolantInjectionSlider;
}
