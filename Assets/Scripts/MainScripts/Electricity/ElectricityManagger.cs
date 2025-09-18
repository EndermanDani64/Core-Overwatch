using UnityEngine;

public class ElectricityManagger : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private BlackoutEvent blackoutEvent;
    [SerializeField] private ShoutSystem shoutSystem;
    [SerializeField] private FanOverwatch fanOverwatch;
    [SerializeField] private GeneratorController generatorController;
    [SerializeField] private EnergyTextUpdater energyTextUpdater;
    [SerializeField] private TempController tempController;

    [Header("Variables")]
    [SerializeField] public static float electricity = ValueStorage.ELECTRICITY_MAX; // ValueStorage.ELECTRICITY_MAX
    //[Header("usageLevel Variables")]
    //public int usageLevel = 0;

    private void Start()
    {
        energyTextUpdater.textUpdate();
    }

    /*[Space]
    public bool areLightsOnline = true; // 1
    public const int lightsCost = 2;
    [Space]
    public bool areDisplaysOnline = true; // 2
    public const int displaysCost = 1;
    [Space]
    public bool areDoorsOnline = true; // 3
    public const int doorsCost = 1;
    [Space]
    public const int fanCost = 1; // 4
    [Space]
    public bool isCoolantFlowOnline = true; // 5
    public const int coolantFlowCost = 3;*/

    /*private bool did1Check = false;
    private bool did2Check = false;
    private bool did3Check = false;
    private bool did4Check = false;
    private bool did5Check = false;
    private int lastFanCount = 0;*/

    public bool enoughEnergy = true;
    /*private void Start()
    {
        if (areLightsOnline && !did1Check)
        {
            usageLevel += lightsCost;
            did1Check = true;
        }
        else if (!areLightsOnline && did1Check)
        {
            did1Check = !did1Check;
        }
        if (areDisplaysOnline && !did2Check)
        {
            usageLevel += displaysCost;
            did2Check = true;
        }
        else if (!areDoorsOnline && did2Check)
        {
            did2Check = !did2Check;
        }
        if (areDoorsOnline && !did3Check)
        {
            usageLevel += doorsCost;
            did3Check = true;
        }
        else if (!areDoorsOnline && did3Check)
        {
            did3Check = !did3Check;
        }
        if (isCoolantFlowOnline && !did4Check)
        {
            usageLevel += coolantFlowCost;
            did4Check = true;
        }
        else if (!isCoolantFlowOnline && did4Check)
        {
            did4Check = !did4Check;
        }
        if (fanOverwatch.FanAmountOnline > 0 && !did5Check)
        {
            usageLevel += fanCost * fanOverwatch.FanAmountOnline;
            lastFanCount = fanOverwatch.FanAmountOnline;
            did5Check = true;
        }
        else if (fanOverwatch.FanAmountOnline < lastFanCount && did5Check)
        {
            did5Check = !did5Check;
        }
    }*/

    public bool isDepletedEnergy = false;
    private float previousElectricity = 0;
    private int cooldown = 10;

    public void IncreaseEnergy(float value, float decreaseValue) // here, we decrease as well
    {
        value -= decreaseValue;
        Debug.LogWarning($"decreaseValue = {decreaseValue} | value = {value}");
        if (cooldown > 0)
        {
            cooldown -= 1;
        }
        float electricityCheck = electricity + value;

        if (electricityCheck <= ValueStorage.ELECTRICITY_MAX && electricityCheck > ValueStorage.ELECTRICITY_MINIMUM)
        {
            electricity += value;
            energyTextUpdater.textUpdate();

            if (isDepletedEnergy && previousElectricity <= ValueStorage.ELECTRICITY_MINIMUM && electricityCheck >= 10)
            {
                isDepletedEnergy = false;
                RestoreEnergy();
            }
        }
        else if (electricityCheck <= ValueStorage.ELECTRICITY_MINIMUM && !isDepletedEnergy && cooldown == 0)
        {
            Debug.LogWarning("1");
            isDepletedEnergy = true;
            DepletedEnergy();
        }
        previousElectricity = electricity;
    }

    public void DecreaseElectricityOnOffline(float value)
    {
        float electricityCheck = electricity - (value * 1.2f);

        if (electricityCheck > ValueStorage.ELECTRICITY_MINIMUM && !isDepletedEnergy)
        {
            electricity -= value;
            energyTextUpdater.textUpdate();
        }
        else if (electricityCheck <= ValueStorage.ELECTRICITY_MINIMUM && !isDepletedEnergy)
        {
            isDepletedEnergy = true;
            DepletedEnergy();
        }
        previousElectricity = electricity;
    }

    private void DepletedEnergy()
    {
        if (!tempController.isMeltdown && !blackoutEvent.isBlackout)
        {
            isDepletedEnergy = true;
            generatorController.ShutDown();
            electricity = ValueStorage.ELECTRICITY_MINIMUM;
            energyTextUpdater.textUpdate();
            blackoutEvent.ForceBlackout();
            shoutSystem.ShowMessage("Energy is depleted. (ElectricityManagger)");
        }
        else
        {
            isDepletedEnergy = true;
            generatorController.ShutDown();
            electricity = ValueStorage.ELECTRICITY_MINIMUM;
            energyTextUpdater.textUpdate();
            blackoutEvent.ForceLightsOut();
        }
    }

    public void RestoreEnergy()
    {
        if (isDepletedEnergy)
        {
            isDepletedEnergy = false;
            blackoutEvent.ForceStop();
            shoutSystem.HideMessage();
        }
    }
}
