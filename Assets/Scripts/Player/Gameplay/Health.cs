using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public RawImage vignetteImage;

    private float timeFallen = 0f;
    private bool isGrounded = false;
    private bool isLiquid = false;
    private float delay = 0f;

    private float vignetteAlpha = 0f;
    private float fadeSpeed = 1.5f; // mennyire gyorsan halványuljon el

    void Update()
    {
        // Ground / Liquid detection
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f, LayerMask.GetMask("Ground"));
        isLiquid = Physics.Raycast(transform.position, Vector3.down, 2f, LayerMask.GetMask("Liquid"));

        // Fall damage
        if (!isGrounded && !isLiquid)
        {
            timeFallen += Time.deltaTime;
        }
        else if (isGrounded)
        {
            if (timeFallen > 0.8f)
            {
                health -= Mathf.Round(1 * timeFallen);
                ShowDamageEffect(0.4f); // enyhe villanás
            }
            timeFallen = 0f;
        }

        // Liquid damage
        if (isLiquid)
        {
            delay += Time.deltaTime;
            if (delay >= 0.5f)
            {
                health -= 5f;
                ShowDamageEffect(0.8f); // erõsebb hatás
                delay = 0f;
            }
        }

        // Fade out the vignette
        vignetteAlpha = Mathf.MoveTowards(vignetteAlpha, 0f, Time.deltaTime * fadeSpeed);
        vignetteImage.color = new Color(1f, 0f, 0f, vignetteAlpha); // piros szín, áttetszõ
    }

    private void ShowDamageEffect(float intensity)
    {
        vignetteAlpha = Mathf.Clamp01(vignetteAlpha + intensity);
    }
}
