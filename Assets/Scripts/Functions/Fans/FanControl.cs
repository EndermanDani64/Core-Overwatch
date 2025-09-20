using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class FanControl : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private Button MainButton;
    [SerializeField] private TMP_Text ButtonText;
    [SerializeField] private FanOverwatch FanOverwatch;

    [Header("Important variables")]
    private bool IsOnline = false;

    void Start()
    {
        ButtonText.text = "Offline";
    }

    public void ToggleFanStatus()
    {
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(ButtonPressCooldown());
    }

    public void Transfer_ForceReset()
    {
        if (IsOnline)
        {
            IsOnline = false;
            FanOverwatch.fanAmountOnline--;
            ButtonText.text = "Offline";
        }
        else
        {
            Debug.LogWarning("The Transfer_ForceReset cannot be called caused by the true IsOnline variable.");
        }
    }

    public void Transfer_ForceStart()
    {
        if (!IsOnline)
        {
            IsOnline = true;
            FanOverwatch.fanAmountOnline++;
            ButtonText.text = "Online";
        }
        else
        {
            Debug.LogWarning("The Transfer_ForceReset cannot be called caused by the false IsOnline variable.");
        }
    }

    private IEnumerator ButtonPressCooldown()
    {
        if (!IsOnline)
        {
            MainButton.interactable = false;
            IsOnline = true;
            FanOverwatch.fanAmountOnline += 1;
            ButtonText.text = "Online";
            yield return new WaitForSeconds(5);
            MainButton.interactable = true;
            yield break;
        }
        else
        {
            MainButton.interactable = false;
            IsOnline = false;
            FanOverwatch.fanAmountOnline -= 1;
            ButtonText.text = "Offline";
            yield return new WaitForSeconds(5);
            MainButton.interactable = true;
            yield break;
        }
    }
}
