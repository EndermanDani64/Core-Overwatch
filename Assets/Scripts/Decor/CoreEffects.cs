using System.Collections;
using UnityEngine;

public class CoreEffects : MonoBehaviour
{
    [SerializeField] private Transform CoreShockWave;  // A gömb referenciája
    [SerializeField] private Vector3 originalSize;
    [SerializeField] private Vector3 growth;           // Mennyivel növekedjen
    [SerializeField] private float duration = 2000f;      // Mennyi ideig tartson egy növekedési fázis

    public IEnumerator CoreShock()
    {
        CoreShockWave.localScale = originalSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration || elapsedTime > duration)
        {
            CoreShockWave.localScale = originalSize + growth * 38 * (elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;  // Várunk egy frame-et
        }

        //CoreShockWave.localScale = originalSize + growth;  // Biztosítjuk, hogy pontosan a célméret legyen
    }
}
