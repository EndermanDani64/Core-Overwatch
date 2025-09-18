using System.Collections.Generic;
using UnityEngine;

public class ValueStorage : MonoBehaviour
{
    public List<int> occupiedIDs = new List<int>();
    /*
     This is the storage for most of the values.
     The game is running on these default values, if you wish to modify these values you should make a backup.
     */

    [Header("Scoreing Values")]
    [SerializeField] public static int COOLANTSUPPLYLEVEL_SCORE_ADD = 45;
    [SerializeField] public static int COOLANTSUPPLYLEVEL_SCORE_SUBTRACT = 15; 

    [Header("MAX Values")]
    [SerializeField] public static int ELECTRICITY_MAX = 250; // 150
    [SerializeField] public static int REACTOR_TMP_MELTINGPOINT = 4000;

    [Header("Electricity Cost Values")]
    [SerializeField] public static int ELECTRICITY_MANUALDOORSCOST = 5; //overall
    [SerializeField] public static int ELECTRICITY_LIGHTCOST = 2; // overall
    [SerializeField] public static int ELECTRICITY_FANCOST = 3; // 3/fan
    [SerializeField] public static int ELECTRICITY_ECOOLANTCOST = 10;
    [SerializeField] public static int ELECTRICITY_ECOOLANT_SUPPLY_CREATION_COST = 60;
    //[SerializeField] public static int ELECTRICITY_

    [SerializeField] public static int ECOOLANT_SUPPLY_DECREASE = 1;
    [SerializeField] public static int ECOOLANT_SUPPLY_ADD = 20;

    [SerializeField] public static int ELECTRICITY_BASE_INCREASE_VALUE = 15; // 2

    [Header("Extra Values")]
    [SerializeField] public static int REACTOR_TMP_MINIMUM = 0;
    [SerializeField] public static int REACTOR_TMP_MAXIMUM = 69420;
    [SerializeField] public static int REACTOR_TMP_RANDOM_DELAY_MIN = 6; // the minimum delay time in seconds that can be randomly chosen
    [SerializeField] public static int REACTOR_TMP_RANDOM_DELAY_MAX = 12; // the maximum delay time in seconds that can be randomly chosen
    [SerializeField] public static int ELECTRICITY_MINIMUM = 0; //0

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

        ECOOLANT_SUPPLY_DECREASE = 1;
        ECOOLANT_SUPPLY_ADD = 20;

        REACTOR_TMP_MINIMUM = 0;
        REACTOR_TMP_MAXIMUM = 69420;
        REACTOR_TMP_RANDOM_DELAY_MIN = 6;
        REACTOR_TMP_RANDOM_DELAY_MAX = 12;
        ELECTRICITY_MINIMUM = 0;

        REACTOR_TMP_DELTA = 2.5f;   
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
