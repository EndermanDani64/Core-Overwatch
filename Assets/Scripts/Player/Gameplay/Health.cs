using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public RawImage vignetteImage;

    private float timeFallen = 0f;
    private bool isGrounded = false;
    private bool isLiquid = false;

    private float vignetteAlpha = 0f;
    private float fadeSpeed = 1.5f; // mennyire gyorsan halványuljon el

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f, LayerMask.GetMask("Ground"));

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

        vignetteAlpha = Mathf.MoveTowards(vignetteAlpha, 0f, Time.deltaTime * fadeSpeed);
        vignetteImage.color = new Color(1f, 0f, 0f, vignetteAlpha); // piros szín, áttetszõ
    }

    private float delay = .5f;

    public void DamagePlayer(int damage, float effectIntensity, bool isLiquidDamage = false , float deltaTime = 0f)
    {
        if (!isLiquidDamage)
        {
            health -= damage;
            ShowDamageEffect(effectIntensity);
        }
        else
        {
            if (delay < .5f)
            {
                delay += deltaTime;
                Debug.Log($"delay: {delay}");
            }
            else
            {
                health -= damage;
                ShowDamageEffect(effectIntensity);
                delay = 0f;
            }
        }
    }

    private void ShowDamageEffect(float intensity)
    {
        vignetteAlpha = Mathf.Clamp01(vignetteAlpha + intensity);
    }
}
