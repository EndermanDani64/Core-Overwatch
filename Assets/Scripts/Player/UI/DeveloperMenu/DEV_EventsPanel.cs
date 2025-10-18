using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.AI;

public class DEV_EventsPanel : MonoBehaviour
{
    [SerializeField] private BlackoutEvent blackoutEvent;
    [SerializeField] private Meltdown meltdownEvent;
    [SerializeField] private OverflowEvent overflowEvent;

    public void StartBlackoutEvent()
    {
        if (!OverallEvents.IsEventRunning)
        {
            StartCoroutine(feedbackTextToggle(blackoutEvent.DEV_ForceBlackout(), "blackout"));
        }
        else
        {
            feedbackTextToggleInfo("The blackout event is already running!");
        }
    }

    public void StartMeltdownEvent()
    {
        if (!OverallEvents.IsMainEventRunning)
        {
            StartCoroutine(feedbackTextToggle(meltdownEvent.DEV_ForceMeltdown(), "meltdown"));
        }
        else
        {
            feedbackTextToggleInfo("The meltdown event is already running!");
        }
    }

    public void StartOverflowEvent()
    {
        if (!OverallEvents.IsEventRunning)
        {
            StartCoroutine(feedbackTextToggle(overflowEvent.DEV_ForceOverflow(), "overflow"));
        }
        else
        {
            feedbackTextToggleInfo("The overflow event is already running!");
        }
    }

    [SerializeField] private TMP_Text feedbackText;
    private IEnumerator feedbackTextToggle(bool resoult, string source)
    {
        if (resoult)
        {
            feedbackText.color = Color.green;
            feedbackText.text = $"The event, {source} has been started!";
        }
        else
        {
            feedbackText.color = Color.red;
            feedbackText.text = $"The event, {source} failed to start!";
        }
        yield return new WaitForSeconds(2);
        feedbackText.text = "";
    }

    private IEnumerator feedbackTextToggleInfo(string text)
    {
        feedbackText.color = Color.yellow;
        feedbackText.text = text;
        yield return new WaitForSeconds(2);
        feedbackText.text = "";
    }
}
