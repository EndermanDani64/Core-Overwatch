using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    public static Dictionary<string, string> DECORATION_WARNINGTEXTS = new Dictionary<string, string>()
    {
        ["minPressure"] = "Minimum pressure levels reached!",
        ["lowESupply"] = "Low Coolant Supply!",
        ["pipeBurst"] = "Pipe burst will occour in t-"
    };
    private static List<string> ongoingWarnings = new List<string>();
    private static List<WarningLight> WarningLights = new();
    private static bool warningLightsEnabled = false;

    // ----  Methods  ---- //

    public static void AddWarning(string id)
    {
        if (!ongoingWarnings.Contains(id))
        {
            ongoingWarnings.Add(id);
        }
        else Debug.LogWarning("Cannot add the warning, because it's already running!");
    }

    /*public static void RemoveWarning(string id)  ---- !!! not finished
    {
        if ()
    }*/

    public static void ActivateWarningLights(string id)
    {
        if (warningLightsEnabled) return;
        foreach (WarningLight wl in WarningLights)
        {
            if (wl.id == id) wl.TurnOnWarningLight();
        }
        warningLightsEnabled = true;
    }

    public static void DeactivateWarningLights(string id)
    {
        if (!warningLightsEnabled) return;
        foreach (WarningLight wl in WarningLights)
        {
            if (wl.id == id) wl.TurnOffWarningLight();
        }
        warningLightsEnabled = false;
    }

    // ----  Submethods  ---- //

    /// <summary>
    /// Collects all the WarningLight components and adds it to the WarningLights array. Call in the Awake() method.
    /// </summary>
    public static void RegisterLight(WarningLight light)
    {
        WarningLights.Add(light);
    }
}