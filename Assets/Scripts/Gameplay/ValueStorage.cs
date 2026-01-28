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

    [Header("Scoreing Values")]
    [SerializeField] public static int SCORE_COOLANTSUPPLYLEVEL_ADD = 45; // i want to rename these
    [SerializeField] public static int SCORE_COOLANTSUPPLYLEVEL_SUBTRACT = 15;
    [SerializeField] public static int SCORE_WORKSHIFT_END = 500;

    [Header("MAX Values")]
    [SerializeField] public static int ELECTRICITY_MAX = 250;
    [SerializeField] public static int EGRID_MAX_CAPACITY = 1250;
    [SerializeField] public static int REACTOR_TMP_MELTINGPOINT = 4000;
    [SerializeField] public static int REACTOR_PS_PRESSURIZED = 800; // 250 or 800
    [SerializeField] public static int REACTOR_PS_MAX = 2000;

    [SerializeField] public static int PLAYER_HEALTH_MAX = 100;

    [Header("Item Values")]
    [SerializeField] public static float ITEM_HAZMATSUIT_PICKUPTIME = 5f;

    [Header("Electricity Cost Values")]
    [SerializeField] public static int ELECTRICITY_MANUALDOORSCOST = 5; //overall
    [SerializeField] public static int ELECTRICITY_LIGHTCOST = 2; // overall
    [SerializeField] public static int ELECTRICITY_FANCOST = 3; // 3/fan
    [SerializeField] public static int ELECTRICITY_ECOOLANTCOST = 10;
    [SerializeField] public static int ELECTRICITY_ECOOLANT_SUPPLY_CREATION_COST = 60;

    [Header("Electricity Decrease Values")]
    [SerializeField] public static int ELECTRICITY_BLACKOUT_DECREASE = 4;

    [SerializeField] public static int COOLANT_SUPPLY_DECREASE = 1;
    [SerializeField] public static int COOLANT_SUPPLY_ADD = 20;

    [SerializeField] public static int ELECTRICITY_BASE_INCREASE_VALUE = 15; // 2
    [SerializeField] public static int ELECTRICITY_TEMP_INCREASE_MIN_VALUE = 0; 
    [SerializeField] public static int ELECTRICITY_TEMP_INCREASE_MAX_VALUE = 100;

    [Header("Time Values")]
    [SerializeField] public static float TIME_FIXABLE_TIMETOFIX = 3f;
    [SerializeField] public static float TIME_FIXABLE_PIPE_TIMETOFIX = 4f;
    [SerializeField] public static float OVERFLOWEVENT_LIQUIDSPEED_INCRESE_PERPIPE = .4f;
    [SerializeField] public static float OVERFLOWEVENT_LIQUIDSPEED_DECRESE = 1f;

    [Header("Health stuff")]
    [SerializeField] public static int HEALTH_LIQUID_DAMAGE = 2;

    [Header("Movment Values")]
    [SerializeField] public static float PLAYER_SPEED_NAKED = 12f; // 12
    [SerializeField] public static float PLAYER_SPEED_NAKED_SPRINT = 16f; // 18
    [SerializeField] public static float PLAYER_SPEED_HAZMAT = 6f;
    [SerializeField] public static float PLAYER_SPEED_HAZMAT_SPRINT = 10f;

    [SerializeField] public static float STAMINA_NAKED_WALK_DECREASE_MULTIPLIER = 8f;
    [SerializeField] public static float STAMINA_HAZMAT_SPRINT_DECREASE_MULTIPLIER = 15f;
    [SerializeField] public static float STAMINA_NAKED_REGEN_MULTIPLIER = 5f;
    [SerializeField] public static float STAMINA_HAZMAT_REGEN_MULTIPLIER = 5f;
    

    [Header("Extra Values")]
    [SerializeField] public static int REACTOR_TMP_MINIMUM = 0;
    [SerializeField] public static int REACTOR_TMP_MAXIMUM = 69420;
    [SerializeField] public static int REACTOR_TMP_RANDOM_DELAY_MIN = 6; // the minimum delay time in seconds that can be randomly chosen
    [SerializeField] public static int REACTOR_TMP_RANDOM_DELAY_MAX = 12; // the maximum delay time in seconds that can be randomly chosen
    [SerializeField] public static int ELECTRICITY_MINIMUM = 0; //0

    [Header("Valid Event Ids")]
    [SerializeField] public static List<string> VALID_EVENT_IDS = new List<string>() { "meltdown", "blackout", "overflow", "overflowWait" };

    [Header("Custom Game Reactor Values")]
    [SerializeField] public static float REACTOR_TMP_DELTA = 2.5f;

    public static void ResetValues()
    {
        ELECTRICITY_MAX = 150;
        REACTOR_TMP_MELTINGPOINT = 4000;

        ELECTRICITY_MANUALDOORSCOST = 5;
        ELECTRICITY_LIGHTCOST = 2;
        ELECTRICITY_FANCOST = 3;
        ELECTRICITY_ECOOLANTCOST = 10;

        COOLANT_SUPPLY_DECREASE = 0;
        COOLANT_SUPPLY_ADD = 20;

        REACTOR_TMP_MINIMUM = 0;
        REACTOR_TMP_MAXIMUM = 69420;
        REACTOR_TMP_RANDOM_DELAY_MIN = 6;
        REACTOR_TMP_RANDOM_DELAY_MAX = 12;
        ELECTRICITY_MINIMUM = 0;

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
            case "ELECTRICITY_MAX":
                ELECTRICITY_MAX = intValue;
                break;
            case "REACTOR_TMP_MELTINGPOINT":
                REACTOR_TMP_MELTINGPOINT = intValue;
                break;
            case "ELECTRICITY_MANUALDOORSCOST":
                ELECTRICITY_MANUALDOORSCOST = intValue;
                break;
            case "ELECTRICITY_LIGHTCOST":
                ELECTRICITY_LIGHTCOST = intValue;
                break;
            case "ELECTRICITY_FANCOST":
                ELECTRICITY_FANCOST = intValue;
                break;
            case "ELECTRICITY_ECOOLANTCOST":
                ELECTRICITY_ECOOLANTCOST = intValue;
                break;
            case "ELECTRICITY_ECOOLANT_SUPPLY_CREATION_COST":
                ELECTRICITY_ECOOLANT_SUPPLY_CREATION_COST = intValue;
                break;
            case "COOLANT_SUPPLY_DECREASE":
                COOLANT_SUPPLY_DECREASE = intValue;
                break;
            case "COOLANT_SUPPLY_ADD":
                COOLANT_SUPPLY_ADD = intValue;
                break;
            case "ELECTRICITY_BASE_INCREASE_VALUE":
                ELECTRICITY_BASE_INCREASE_VALUE = intValue;
                break;
            case "REACTOR_TMP_MINIMUM":
                REACTOR_TMP_MINIMUM = intValue;
                break;
            case "REACTOR_TMP_MAXIMUM":
                REACTOR_TMP_MAXIMUM = intValue;
                break;
            case "REACTOR_TMP_RANDOM_DELAY_MIN":
                REACTOR_TMP_RANDOM_DELAY_MIN = intValue;
                break;
            case "REACTOR_TMP_RANDOM_DELAY_MAX":
                REACTOR_TMP_RANDOM_DELAY_MAX = intValue;
                break;
            case "ELECTRICITY_MINIMUM":
                ELECTRICITY_MINIMUM = intValue;
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
