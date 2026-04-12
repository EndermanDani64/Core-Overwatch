using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ECoolantButton : MonoBehaviour
{
    [SerializeField] private ECoolantController controller;
    [SerializeField] private Image progressBar;
    [SerializeField] private Image progressBarFill;

    private bool isIncreasing = false;
    private float value = 0f;
    private float maxHeight;
    private Coroutine progressCoroutine;

    private void Start()
    {
        maxHeight = progressBar.rectTransform.rect.height * 300f; // 300x-os magasság
        RectTransform fillTransform = progressBarFill.rectTransform;

        // **Alulról kezdje a feltöltést**
        fillTransform.pivot = new Vector2(0.5f, 0f);
        fillTransform.anchoredPosition = new Vector2(0, 0);

        // **Biztosítsuk, hogy ne legyen túl kicsi az elején**
        fillTransform.sizeDelta = new Vector2(fillTransform.sizeDelta.x, 30f);
    }

    private IEnumerator ProgressBarIncrease()
    {
        RectTransform fillTransform = progressBarFill.rectTransform;

        while (controller.isECoolantEventActive)
        {
            if (isIncreasing)
            {
                value = Mathf.Clamp(value + 0.05f, 0f, 1f);
            }
            else
            {
                value = Mathf.Clamp(value - 0.05f, 0f, 1f);
            }

            float newHeight = Mathf.Max(value * maxHeight, 30f);
            fillTransform.sizeDelta = new Vector2(fillTransform.sizeDelta.x, newHeight);

            //Debug.Log($"Progress: {value}");

            yield return new WaitForSeconds(0.35f);
        }

        if (value > -1) // value > 0.5f && value < 0.8f
        {
            controller.RodSuccess += 1;
        }
    }

    public void ToggleProgress()
    {
        isIncreasing = !isIncreasing;

        if (progressCoroutine == null)
        {
            progressCoroutine = StartCoroutine(ProgressBarIncrease());
        }
    }
}
