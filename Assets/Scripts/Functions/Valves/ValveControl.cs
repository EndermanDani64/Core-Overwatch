using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ValveControl : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private Button MainButton;
    [SerializeField] private TMP_Text ButtonText;
    [SerializeField] private ValveOverwatch ValveOverwatch;

    [Header("Important variables")]
    private bool IsOpen = false;

    void Start()
    {
        ButtonText.text = "Closed";
    }

    public void ToggleValveStatus()
    {
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(ButtonPressCooldown());
    }

    public void Transfer_ForceReset()
    {
        if (IsOpen)
        {
            IsOpen = false;
            ValveOverwatch.valveAmountOpen--;
            ButtonText.text = "Closed";
        }
        else
        {
            Debug.LogWarning("The Transfer_ForceReset cannot be called caused by the true IsOpen variable.");
        }
    }

    public void Transfer_ForceStart()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            ValveOverwatch.valveAmountOpen++;
            ButtonText.text = "Open";
        }
        else
        {
            Debug.LogWarning("The Transfer_ForceReset cannot be called caused by the false IsOpen variable.");
        }
    }

    private IEnumerator ButtonPressCooldown()
    {
        if (!IsOpen)
        {
            MainButton.interactable = false;
            IsOpen = true;
            ValveOverwatch.valveAmountOpen += 1;
            ButtonText.text = "Open";
            yield return new WaitForSeconds(5);
            MainButton.interactable = true;
            yield break;
        }
        else
        {
            MainButton.interactable = false;
            IsOpen = false;
            ValveOverwatch.valveAmountOpen -= 1;
            ButtonText.text = "Closed";
            yield return new WaitForSeconds(5);
            MainButton.interactable = true;
            yield break;
        }
    }
}

