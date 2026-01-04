using System.IO;
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
    [SerializeField] private OverallEvents overallEvents;

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
    public const int coolantFlowCost = 3;

    private bool did1Check = false;
    private bool did2Check = false;
    private bool did3Check = false;
    private bool did4Check = false;
    private bool did5Check = false;
    private int lastFanCount = 0;*/

    public bool enoughEnergy = true;

    public float usage = 0f;

    public bool isDepletedEnergy = false;
    private float previousElectricity = 0;
    // private int cooldown = 10; ???

    /// <summary>
    /// Function for increasing the electricity on a specific increasing rate.
    /// </summary>
    /// <param name="value">The rate of electricity increase.</param>
    /// <param name="multiplier">The rate of electricity increasing's multiplier.</param>
    public void IncreaseElectricity(float value, float multiplier = 1f)
    {
        /*if (cooldown > 0) ???
        {
            cooldown -= 1; ???
        }*/

        // electricity + (value * multiplier) = checking if the modification isn't going to be unnatural
        if (electricity + (value * multiplier) < ValueStorage.ELECTRICITY_MAX && electricity + (value * multiplier) > ValueStorage.ELECTRICITY_MINIMUM)
        {
            electricity += value * multiplier;
            energyTextUpdater.textUpdate();

            // Debug.Log($"isDepletedEnergy = {isDepletedEnergy} | previousElectricity > ValueStorage.ELECTRICITY_MINIMUM = {previousElectricity > ValueStorage.ELECTRICITY_MINIMUM}");
            // Debug.Log($"isDepletedEnergy whole = {isDepletedEnergy && previousElectricity > ValueStorage.ELECTRICITY_MINIMUM}");

            if (isDepletedEnergy && previousElectricity > ValueStorage.ELECTRICITY_MINIMUM)
            {
                Debug.Log("nice");
                isDepletedEnergy = false;
                RestoreEnergy();
            }
        }
        else if (electricity + (value * multiplier) <= ValueStorage.ELECTRICITY_MINIMUM && !isDepletedEnergy /*??? && cooldown == 0 ???*/)
        {
            Debug.Log("not that nice");
            isDepletedEnergy = true;
            DepletedEnergy();
        }
        previousElectricity = electricity;
    }

    /// <summary>
    /// Function for decreasing the electricity on a specific decreasing rate.
    /// </summary>
    /// <param name="value">The rate of electricity decrease.</param>
    /// <param name="multiplier">The rate of electricity decreasing's multiplier.</param>
    public void DecreaseElectricity(float value, float multiplier = 1f)
    {
        // electricity - (value * multiplier) = checking if the modification isn't going to be unnatural
        if (electricity - (value * multiplier) > ValueStorage.ELECTRICITY_MINIMUM)
        {
            electricity -= value * multiplier;
            energyTextUpdater.textUpdate();
        }
        else if (electricity - (value * multiplier) <= ValueStorage.ELECTRICITY_MINIMUM && !isDepletedEnergy)
        {
            isDepletedEnergy = true;
            DepletedEnergy();
        }
        previousElectricity = electricity;
    }

    private void DepletedEnergy()
    {
        if (!tempController.isMeltdown && !OverallEvents.IsBlackout)
        {
            isDepletedEnergy = true;
            generatorController.ShutDown();
            electricity = ValueStorage.ELECTRICITY_MINIMUM;
            energyTextUpdater.textUpdate();
            overallEvents.PlayEvent("blackout");
            shoutSystem.ShowMessage("Energy is depleted. Generator has shut down!");
        }
    }

    public void RestoreEnergy()
    {
        blackoutEvent.ForceStop();
        shoutSystem.HideMessage();
    }
}
