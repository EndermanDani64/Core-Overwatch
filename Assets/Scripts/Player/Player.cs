using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Player : MonoBehaviour
{
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
    public static void SetHolding(string item)
    {
        if (ValueStorage.VALID_ITEM_IDS.Contains(item)) holding = item;
        else Debug.LogWarning("The set item in SetHolding() is not valid.");
    }
    /// <summary>
    /// Sets the holding variable to "". (Holding is the item ID which the player is holding in their hand.)
    /// </summary>
    public static void SetHoldingToNone()
    {
        holding = "";
    }

    // ----  Suits  ---- //
    public static void SetWearing(string suit)
    {
        if (ValueStorage.VALID_WEARABLE_IDS.Contains(suit)) wearing = suit;
        else Debug.LogWarning("The set suit in SetWearing() is not valid.");
    }
    /// <summary>
    /// Sets the wearing variable to "".
    /// </summary>
    public static void SetWearingToNone()
    {
        wearing = "";
    }

    // ----  Movment  ---- //

    public static float stamina = 100f;

    /// <summary>
    /// Disables the movment, and shows the cursor.
    /// </summary>
    public static void EnableDefaultInput()
    {
        if (_fpsInputModule == null || _defaultInputModule == null)
        {
            Debug.LogError("Input modules are not initialized on Player.");
            return;
        }

        _fpsInputModule.enabled = false;
        _defaultInputModule.enabled = true;
    }

    /// <summary>
    /// Enables the movment, and hides the cursor.
    /// </summary>
    public static void DisableDefaultInput()
    {
        if (_fpsInputModule == null || _defaultInputModule == null)
        {
            Debug.LogError("Input modules are not initialized on Player.");
            return;
        }

        _fpsInputModule.enabled = true;
        _defaultInputModule.enabled = false;
    }

    /// <summary>
    /// Returns the GameObject which the player is looking at. If none then returns with null.
    /// </summary>
    public static GameObject GetLookedAtObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    /// <summary>
    /// Returns a bool wether the player is looking at the specified GameObject or not.
    /// </summary>
    public static bool LookingAtThis(GameObject target, KeyCode targetKey = KeyCode.None)
    {
        if (targetKey != KeyCode.None)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == target) return true;
            }
            return false;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && Input.GetKey(targetKey))
            {
                if (hit.collider.gameObject == target) return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Takes a string parameter and then compares it to the object's tag which the player is looking at. True if matches.
    /// </summary>
    /// <returns></returns>
    public static bool LookingAtTag(string targetTag, KeyCode targetKey = KeyCode.None)
    {
        if (targetTag == "" || targetTag == null) 
        {
            Debug.LogWarning("Parameter targetTag is not set or empty! Returned with false.");
            return false;
        }

        if (targetKey == KeyCode.None)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    return true;
                }
            }
        }
        else if (targetKey != KeyCode.None && Input.GetKey(targetKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void Start()
    {
        //LookingAtFixable
    }

    public delegate void LooksAt();
    public static event LooksAt LookingAtFixable;

    [SerializeField] public static FPSInputModule _fpsInputModule;
    [SerializeField] public static InputSystemUIInputModule _defaultInputModule;
}
