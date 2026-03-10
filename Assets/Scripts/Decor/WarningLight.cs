using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarningLight : MonoBehaviour
{
    public string id = "";
    private bool enabled = false;

    // ----  Methods  ---- //

    /// <summary>
    /// Turns on the WarningLights.
    /// </summary>
    public void TurnOnWarningLight()
    {
        Debug.LogWarning("entered $$$");
        if (enabled) Debug.LogWarning("Cannot turn on the warning lights because they are already on.");
        else
        {
            Debug.LogWarning("$");
            enabled = true;
            StartCoroutine(WarningFlicker());
        }
    }

    /// <summary>
    /// Turns off the WarningLights.
    /// </summary>
    public void TurnOffWarningLight()
    {
        if (!enabled) Debug.LogWarning("Cannot turn off the warning lights because they are already off.");
        else
        {
            enabled = false;
        }
    }

    // ----  Submethods  ---- //

    public IEnumerator WarningFlicker()
    {
        if (!enabled) Debug.LogWarning("Cannot start corroutine because enabled is false");
        while (enabled)
        {
            if (light.material == on) { light.material = off; }
            else { light.material = on; }
            yield return new WaitForSeconds(.5f);
        }
        light.material = off;
    }

    private void Awake()
    {
        WarningManager.RegisterLight(gameObject.GetComponent<WarningLight>());
    }

    [SerializeField] private Image light;
    [SerializeField] private Material on;
    [SerializeField] private Material off;
}
