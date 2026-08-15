using UnityEngine;
using TMPro;
using System.Collections;

public class GeneratorController : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private FanOverwatch FanOverwatch; // <- rework needed + RENAME
    [SerializeField] private ShoutSystem shoutSystem; // <- rework + RENAME

    [Header("Generator UIs")]
    public UnityEngine.UI.Button StartStopButton;
    public TMP_Text GeneratorStatus;

    [Header("Generator variables")]
    public static bool isGeneratorOnline = true;
    public bool isGeneratorDestroyed = false;

    public float GeneratorEnergyProduction = 0f; // public -> for showing the production on a gui

    private void Start()
    {
        ReactorManager.OnReactorStart += () => { GameTimeManager.OnTickUpdate += GeneratorMain; };
    }


    private bool _energyWarningShown = false;
    private float _lastProdactionValueAdded = 0f;

    public void GeneratorMain(float tickDeltaTime)
    {
        if (isGeneratorOnline && !OverallEvents.IsBlackout)
        {
            GeneratorEnergyProduction = ((ReactorManager.ReactorData.Temperature / ValueStorage.REACTOR_TMP_MELTINGPOINT) * 100) * ValueStorage.ENERGY_BASE_PRODUCTION_VALUE;
            GeneratorEnergyProduction *= tickDeltaTime;
            GeneratorEnergyProduction = Mathf.Min(GeneratorEnergyProduction, ValueStorage.ENERGY_PRODUCTION_MAX_VALUE);
        }
        else if (!isGeneratorOnline && !OverallEvents.IsBlackout)
        {
            GeneratorEnergyProduction = 0f;
        }

        EnergyManagger.MasterBatteryData.EnergyProduction -= _lastProdactionValueAdded;
        EnergyManagger.MasterBatteryData.EnergyProduction += GeneratorEnergyProduction;
        _lastProdactionValueAdded = GeneratorEnergyProduction;

        if (CanSendWarningMessage())
        {
            shoutSystem.SendMessage("Watch the decreasing energy levels.", 8f, 0f);
            _MessageTimeout(32f);
        }
    }

    public void ToggleGeneratorStatus() // connected through unity
    {
        if (!isGeneratorDestroyed)
        {
            StartCoroutine(GeneratorToggleAndSwitchManager());
        }
    }

    
    private IEnumerator GeneratorToggleAndSwitchManager() // We press the on/off button
    {
        if (StartStopButton.enabled)
        {
            StartStopButton.interactable = false;

            if (!isGeneratorOnline && !isGeneratorDestroyed)
            {
                GeneratorStatus.text = "Starting up...";

                yield return new WaitForSeconds(5);

                GeneratorStatus.text = "Online";
                isGeneratorOnline = true;
            } 
            else if (isGeneratorOnline && !isGeneratorDestroyed)
            {
                GeneratorStatus.text = "Shutting down...";

                yield return new WaitForSeconds(5);

                GeneratorStatus.text = "Offline";
                isGeneratorOnline = false;
            }

            StartStopButton.interactable = true;
        }
    }


    
    // -- warning message stuff -- //

    private bool CanSendWarningMessage()
    {
        return EnergyManagger.MasterBatteryData.EnergyProduction < EnergyManagger.MasterBatteryData.EnergyConsumption && 
            !OverallEvents.IsEventRunning && 
            !_energyWarningShown;
    }

    private IEnumerator _MessageTimeout(float time)
    {
        _energyWarningShown = true;
        yield return new WaitForSeconds(time);
        _energyWarningShown = false;
    }
}
