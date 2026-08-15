using System.Collections.Generic;
using UnityEngine;

public class ValueStorage : MonoBehaviour
{
    /*
       <------------------------------------------------!!!------------------------------------------------>
                                  This is the storage for most of the values.
     The game is running on these default values, if you wish to modify these values you should make a backup.
       <------------------------------------------------!!!------------------------------------------------>
     */

    public List<int> occupiedIDs = new List<int>();
    
    [Header("Map Values")]
    [SerializeField] public static float MAP_DEFAULT_LIGHT_INTENSITY = 25.05f;


    [Header("Player Values")]
    [SerializeField] public static int PLAYER_HEALTH_MAX = 100;
    [SerializeField] public static int HEALTH_LIQUID_DAMAGE = 2;


    [Header("Movment Values")]
    [SerializeField] public static float PLAYER_SPEED_NAKED = 12f; // 12
    [SerializeField] public static float PLAYER_SPEED_NAKED_SPRINT = 16f; // 18
    [SerializeField] public static float PLAYER_SPEED_HAZMAT = 6f;
    [SerializeField] public static float PLAYER_SPEED_HAZMAT_SPRINT = 10f;
    [Space]
    [SerializeField] public static float STAMINA_NAKED_WALK_DECREASE_MULTIPLIER = 8f;
    [SerializeField] public static float STAMINA_HAZMAT_SPRINT_DECREASE_MULTIPLIER = 15f;
    [SerializeField] public static float STAMINA_NAKED_REGEN_MULTIPLIER = 5f;
    [SerializeField] public static float STAMINA_HAZMAT_REGEN_MULTIPLIER = 5f;


    [Header("Scoreing Values")]
    [SerializeField] public static int SCORE_COOLANTSUPPLYLEVEL_ADD = 45; // i want to rename these
    [SerializeField] public static int SCORE_COOLANTSUPPLYLEVEL_SUBTRACT = 15;
    [SerializeField] public static int SCORE_WORKSHIFT_END = 500;


    [Header("Reactor Values")]
    [SerializeField] public static int REACTOR_TMP_MIN = 0;
    [SerializeField] public static int REACTOR_TMP_MAX = 69420;
    [SerializeField] public static int REACTOR_TMP_MELTINGPOINT = 4000;
    [SerializeField] public static int REACTOR_TMP_DEFAULT = 20;
    [SerializeField] public static int REACTOR_TMP_RANDOM_DELAY_MIN = 6; // the minimum delay time in seconds that can be randomly chosen
    [SerializeField] public static int REACTOR_TMP_RANDOM_DELAY_MAX = 12; // the maximum delay time in seconds that can be randomly chosen
    [SerializeField] public static int REACTOR_PS_MIN = 0;
    [SerializeField] public static int REACTOR_PS_MAX = 2000;
    [SerializeField] public static int REACTOR_PS_PRESSURIZED = 800; // 250 or 800
    [SerializeField] public static int REACTOR_PS_DEFAULT = 2;
    [SerializeField] public static int REACTOR_PS_TOGGLEFIXABLESTRESHOLD = 50; // 50 or 80
    [Space]
    [SerializeField] public static int COOLANT_SUPPLY_DECREASE = 1;
    [SerializeField] public static int COOLANT_SUPPLY_ADD = 20;


    [Header("Custom Game Reactor Values")]
    [SerializeField] public static float REACTOR_TMP_DELTA = 2.5f;


    [Header("Energy Values")]
    [SerializeField] public static int ENERGY_MINIMUM = 0;
    [SerializeField] public static int ENERGY_MAXIMUM = 250;
    [SerializeField] public static float ENERGY_BASE_PRODUCTION_VALUE = 0.35f;
    [SerializeField] public static int ENERGY_PRODUCTION_MAX_VALUE = 150;
    [Space]
    [SerializeField] public static float ENERGY_CONSUMPTION_MANUALDOORS = 5; //overall
    [SerializeField] public static float ENERGY_CONSUMPTION_LIGHT = 0.5f; // overall
    [SerializeField] public static float ENERGY_CONSUMPTION_FAN = 3; // per fan
    [SerializeField] public static int ENERGY_CONSUMPTION_COOLANT_CREATION = 30;

    [Header("Item Values")]
    [SerializeField] public static float ITEM_HAZMATSUIT_PICKUPTIME = 5f;

    [Header("Fixable Values")]
    [SerializeField] public static int FIXABLES_MAXTASK = 1;
    [SerializeField] public static float FIXABLES_COOLDOWNBETWEENDAMAGING = 120f;


    [Header("Time Values")]
    [SerializeField] public static float TIME_FIXABLE_TIMETOFIX = 2f; // 3
    [SerializeField] public static float TIME_FIXABLE_PIPE_TIMETOFIX = 4f;
    [SerializeField] public static float OVERFLOWEVENT_LIQUIDSPEED_INCRESE_PERPIPE = .4f;
    [SerializeField] public static float OVERFLOWEVENT_LIQUIDSPEED_DECRESE = 1f;

    [Header("Interactible LookingAt Texts")]
    [SerializeField] public static string INTERACTIBLE_LOOKINGATTEXT = ": Press To Interact";
    [SerializeField] public static string INTERACTIBLE_NOTENABLED = "Can't press...";

    [Header("Valid Ids")]
    [SerializeField] public static List<string> VALID_EVENT_IDS = new List<string>() { "meltdown_first", "meltdown_second", "blackout", "overflow", "overflowWait" };
    [SerializeField] public static List<string> VALID_ITEM_IDS = new List<string>() { "", "ECoolantSupply" };
    [SerializeField] public static List<string> VALID_WEARABLE_IDS = new List<string>() { "", "hazmatSuit" };

    //[SerializeField] public static int EGRID_MAX_CAPACITY = 1250;

    public static void ResetValues()
    {
        ENERGY_MAXIMUM = 150;
        REACTOR_TMP_MELTINGPOINT = 4000;

        ENERGY_CONSUMPTION_MANUALDOORS = 5;
        ENERGY_CONSUMPTION_LIGHT = 2;
        ENERGY_CONSUMPTION_FAN = 3;

        COOLANT_SUPPLY_DECREASE = 0;
        COOLANT_SUPPLY_ADD = 20;

        REACTOR_TMP_MIN = 0;
        REACTOR_TMP_MAX = 69420;
        REACTOR_TMP_RANDOM_DELAY_MIN = 6;
        REACTOR_TMP_RANDOM_DELAY_MAX = 12;
        ENERGY_MINIMUM = 0;

        REACTOR_TMP_DELTA = 2.5f;   
    }

    public void UpdateValue(string key, int intValue = 0, float floatValue = 0.0f)
    {
        switch (key)
        {
            case "COOLANTSUPPLYLEVEL_SCORE_ADD":
                SCORE_COOLANTSUPPLYLEVEL_ADD = intValue;
                break;
            case "COOLANTSUPPLYLEVEL_SCORE_SUBTRACT":
                SCORE_COOLANTSUPPLYLEVEL_SUBTRACT = intValue;
                break;
            case "ENERGY_MAX":
                ENERGY_MAXIMUM = intValue;
                break;
            case "REACTOR_TMP_MELTINGPOINT":
                REACTOR_TMP_MELTINGPOINT = intValue;
                break;
            case "ENERGY_MANUALDOORSCOST":
                ENERGY_CONSUMPTION_MANUALDOORS = intValue;
                break;
            case "ENERGY_LIGHTCOST":
                ENERGY_CONSUMPTION_LIGHT = intValue;
                break;
            case "ENERGY_FANCOST":
                ENERGY_CONSUMPTION_FAN = intValue;
                break;
            case "ENERGY_ECOOLANT_SUPPLY_CREATION_COST":
                ENERGY_CONSUMPTION_COOLANT_CREATION = intValue;
                break;
            case "COOLANT_SUPPLY_DECREASE":
                COOLANT_SUPPLY_DECREASE = intValue;
                break;
            case "COOLANT_SUPPLY_ADD":
                COOLANT_SUPPLY_ADD = intValue;
                break;
            case "ENERGY_BASE_INCREASE_VALUE":
                ENERGY_BASE_PRODUCTION_VALUE = intValue;
                break;
            case "REACTOR_TMP_MINIMUM":
                REACTOR_TMP_MIN = intValue;
                break;
            case "REACTOR_TMP_MAXIMUM":
                REACTOR_TMP_MAX = intValue;
                break;
            case "REACTOR_TMP_RANDOM_DELAY_MIN":
                REACTOR_TMP_RANDOM_DELAY_MIN = intValue;
                break;
            case "REACTOR_TMP_RANDOM_DELAY_MAX":
                REACTOR_TMP_RANDOM_DELAY_MAX = intValue;
                break;
            case "ENERGY_MINIMUM":
                ENERGY_MINIMUM = intValue;
                break;
            case "REACTOR_TMP_DELTA":
                REACTOR_TMP_DELTA = floatValue;
                break;
        }
    }

    public void ReserveID(int id)
    {
        if (!occupiedIDs.Contains(id))
        {
            occupiedIDs.Add(id);
            Debug.Log($"ID {id} lefoglalva.");
        }
        else
        {
            Debug.LogWarning($"ID {id} már foglalt!");
        }
    }

    public void ReleaseID(int id)
    {
        if (occupiedIDs.Contains(id))
        {
            occupiedIDs.Remove(id);
            Debug.Log($"ID {id} felszabadítva.");
        }
        else
        {
            Debug.LogWarning($"ID {id} nem volt foglalt.");
        }
    }

    public bool IsIDFree(int id)
    {
        return !occupiedIDs.Contains(id);
    }
}
