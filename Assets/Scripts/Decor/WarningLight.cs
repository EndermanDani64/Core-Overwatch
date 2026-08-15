using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarningLight : MonoBehaviour
{
    public string id = "";
    private bool enabld = false;

    // ----  Methods  ---- //

    /// <summary>
    /// Turns on the WarningLights.
    /// </summary>
    public void TurnOnWarningLight()
    {
        Debug.LogWarning("entered $$$");
        if (enabld) Debug.LogWarning("Cannot turn on the warning lights because they are already on.");
        else
        {
            Debug.LogWarning("$");
            enabld = true;
            StartCoroutine(WarningFlicker());
        }
    }

    /// <summary>
    /// Turns off the WarningLights.
    /// </summary>
    public void TurnOffWarningLight()
    {
        if (!enabld) Debug.LogWarning("Cannot turn off the warning lights because they are already off.");
        else
        {
            enabld = false;
        }
    }

    // ----  Submethods  ---- //

    public IEnumerator WarningFlicker()
    {
        if (!enabld) Debug.LogWarning("Cannot start corroutine because \"enabl\" is false");
        while (enabld)
        {
            if (imageLight.material == on) { imageLight.material = off; }
            else { imageLight.material = on; }
            yield return new WaitForSeconds(.5f);
        }
        imageLight.material = off;
    }

    private void Awake()
    {
        WarningManager.RegisterLight(gameObject.GetComponent<WarningLight>());
    }

    [SerializeField] private Image imageLight;
    [SerializeField] private Material on;
    [SerializeField] private Material off;
}
