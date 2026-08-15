using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class EnergyManagger : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private ShoutSystem shoutManager; // <- REWORK
    [SerializeField] private FanOverwatch fanOverwatch; // <- REWORK

    public static MasterBatteryData MasterBatteryData;

    [Header("Variables")]
    [SerializeField] public static float electricity = ValueStorage.ENERGY_MAXIMUM;



    private void Start()
    {
        MasterBatteryData = new MasterBatteryData();

        GameTimeManager.OnTickUpdate += TickEnergyChange;
    }

    public void ConsumeEnergy(float amount)
    {
        float energyCheck = MasterBatteryData.Energy - amount;

        MasterBatteryData.Energy = Mathf.Min(ValueStorage.ENERGY_MINIMUM, energyCheck);

        if (MasterBatteryData.Energy == ValueStorage.ENERGY_MINIMUM)
            EnergyDepletion();
    }


    private bool _depletionHappened;
    public void TickEnergyChange(float tickDeltaTime)
    {
        float energyChange = MasterBatteryData.EnergyProduction - MasterBatteryData.EnergyConsumption;
        energyChange *= tickDeltaTime;


        Debug.Log($"EnergyProduction: {MasterBatteryData.EnergyProduction} | FacilityGridData.EnergyConsumption: {MasterBatteryData.EnergyConsumption}");


        MasterBatteryData.Energy = Mathf.Min(
            Mathf.Max(ValueStorage.ENERGY_MINIMUM, MasterBatteryData.Energy + energyChange), 
            ValueStorage.ENERGY_MAXIMUM
        );


        if (MasterBatteryData.Energy == ValueStorage.ENERGY_MINIMUM)
        {
            EnergyDepletion();
        }
        else if (MasterBatteryData.Energy != ValueStorage.ENERGY_MINIMUM && _depletionHappened)
        {
            EnergyRestore();
            _depletionHappened = false;
        }
    }


    
    private void EnergyDepletion()
    {
        if (_depletionHappened) return;

        facilityLightsManager.LightOutage();
        _depletionHappened = true;
    }

    private void EnergyRestore()
    {
        facilityLightsManager.LightRestore();
    }

    // -- References -- //

    [SerializeField] private FacilityLightsManager facilityLightsManager;

}


public class MasterBatteryData
{
    public float Energy
    {
        get => _energy;
        set
        {
            if (value >= ValueStorage.ENERGY_MAXIMUM)
            {
                _energy = ValueStorage.ENERGY_MAXIMUM;
            }
            else if (value <= ValueStorage.ENERGY_MINIMUM)
            {
                _energy = ValueStorage.ENERGY_MINIMUM;
            }
            else
            {
                _energy = value;
            }
        }
    }
    /// <summary>
    /// Only positive value.
    /// </summary>
    public float EnergyConsumption
    {
        get => _energyConsumption;
        set => _energyConsumption = Mathf.Abs(value);
    }
    /// <summary>
    /// Only positive value.
    /// </summary>
    public float EnergyProduction
    {
        get => _energyProduction;
        set => _energyProduction = Mathf.Abs(value);
    }

    private float _energy;
    private float _energyConsumption;
    private float _energyProduction;
}
