using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class AlarmPanel : MonoBehaviour
{
    [SerializeField] private Image ColorPanel;
    private bool isFlashing = false;
    public float delay = 0.5f;

    void Start()
    {
        if (ColorPanel == null)
        {
            Debug.LogError("ColorPanel nincs beállítva az Inspectorban!");
            return;
        }
        ColorPanel.color = Color.black;
    }

    public void StartAlarm_Meltdown()
    {
        if (!isFlashing) // Ha már fut, ne indítsd újra
        {
            isFlashing = true;
            StartCoroutine(FlashAlarm_Meltdown());
        }
    }

    public void StartAlarm_ECoolantDepleted()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashAlarm_ECoolantDepleted());
        }
    }

    public void StopAlarm()
    {
        StopAllCoroutines();
        isFlashing = false;
        ColorPanel.color = Color.black;
    }

    private IEnumerator FlashAlarm_Meltdown()
    {
        while (isFlashing)
        {
            ColorPanel.color = Color.red;
            yield return new WaitForSeconds(delay);
            ColorPanel.color = Color.black;
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator FlashAlarm_ECoolantDepleted()
    {
        while (isFlashing)
        {
            ColorPanel.color = new Color(220, 236, 0);
            yield return new WaitForSeconds(.75f);
            ColorPanel.color = Color.black;
            yield return new WaitForSeconds(.75f);
        }
    }
}
