using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player : MonoBehaviour
{
    public static FPSInputModule _fpsInputModule;
    public static InputSystemUIInputModule _defaultInputModule;

    public static float health = 100f;
    public static bool isGrounded = false;
    public static bool isLiquid = false;


    // ----  Inventory and Suits  ---- //

    public static bool handsFree = true;
    public static string holding = "";

    public static string wearing = "";
    public static bool naked
    {
        get
        {
            if (wearing == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // ----  Items  ---- //

    /// <summary>
    /// Sets the holding variable to the parameter "item". (Holding is the item ID which the player is holding in their hand.)
    /// </summary>
    public static void ISetHolding(string item)
    {
        if (ValueStorage.VALID_ITEM_IDS.Contains(item)) holding = item;
        else Debug.LogWarning("The set item in SetHolding() is not valid.");
    }
    /// <summary>
    /// Sets the holding variable to "". (Holding is the item ID which the player is holding in their hand.)
    /// </summary>
    public static void ISetHoldingToNone()
    {
        holding = "";
    }

    // ----  Suits  ---- //
    public static void ISetWearing(string suit)
    {
        if (ValueStorage.VALID_WEARABLE_IDS.Contains(suit)) wearing = suit;
        else Debug.LogWarning("The set suit in SetWearing() is not valid.");
    }
    /// <summary>
    /// Sets the wearing variable to "".
    /// </summary>
    public static void ISetWearingToNone()
    {
        wearing = "";
    }

    // ----  Movment  ---- //

    public static float stamina = 100f;

    /// <summary>
    /// Disables the movment, and shows the cursor.
    /// </summary>
    public static void IEnableDefaultInput()
    {
        _fpsInputModule.enabled = false;
        _defaultInputModule.enabled = true;
    }

    /// <summary>
    /// Enables the movment, and hides the cursor.
    /// </summary>
    public static void IDisableDefaultInput()
    {
        _fpsInputModule.enabled = true;
        _defaultInputModule.enabled = false;
    }
}
