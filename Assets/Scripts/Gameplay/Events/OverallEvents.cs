using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OverallEvents : MonoBehaviour
{
    public static bool IsMainEventRunning = false;
    public static bool IsEventRunning = false;
    public static bool IsOverflow = false;
    public static bool IsBlackout = false;
    public static bool IsMeltdown = false;

    public event Action<bool> BlackoutEvent_SatusChange;

    [SerializeField] public Meltdown Event_Meltdown;
    [SerializeField] public BlackoutEvent Event_Blackout;
    [SerializeField] public OverflowEvent Event_Overflow;

    public static List<string> EventQueue = new();

    private void Update()
    {
        if (IsMainEventRunning || IsOverflow)
        {
            IsEventRunning = true;
        }
        else
        {
            IsEventRunning = false;
        }
    }

    /// <summary>
    /// Triggers the in-game event related to the eventId.
    /// </summary>
    /// <param name="eventId">The Id that is refering to a specific in-game event.</param>
    public void TriggerEvent(string eventId)
    {
        if (!ValueStorage.VALID_EVENT_IDS.Contains(eventId))
        {
            Debug.LogWarning($"{eventId} is an unknown eventId.");
        }
        else
        {
            if (eventId == "meltdown")
            {
                Event_Meltdown.ForceMeltdown();
            }
            else if (eventId == "blackout")
            {
                Event_Blackout.ForceBlackout();
                BlackoutEvent_SatusChange?.Invoke(true);
            }
            else if (eventId == "overflow")
            {
                if (!Event_Overflow.IsOverflowEventCooldown)
                {
                    Event_Overflow.ForceOverflow();
                }
            }
            else if (eventId == "overflowWait") 
            {
                Debug.Log("Triggered the overflowWait event.");
                Event_Overflow.ForceOverflowWait();
            }
        }
    }

    /// <summary>
    /// Adds the event related to the eventId to the EventQueue.
    /// </summary>
    /// <param name="eventId">The reference Id to the event.</param>
    public void AddEventToQueue(string eventId)
    {
        if (!ValueStorage.VALID_EVENT_IDS.Contains(eventId))
        {
            Debug.LogWarning($"{eventId} is an unknown eventId.");
        }
        else
        {
            if (EventQueue.Count == 0 && !IsEventRunning)
            {
                EventQueue.Add(eventId);
                TriggerEvent(eventId);
            }
            else
            {
                EventQueue.Add(eventId);
            }

        }
    }

    /// <summary>
    /// Triggers the next in-game event in the EventQueue.
    /// </summary>
    public void EventQueue_TriggerNext()
    {
        foreach(string item in EventQueue)
        {
            Debug.LogWarning($"HERE - van eventQueue-ben: {item} | BEFORE");
        }

        if (EventQueue.Count >= 1) //  && !IsEventRunning
        {
            Debug.Log($"Triggering next event by the id: {EventQueue[0]} and removing from the position 0.");
            EventQueue.RemoveAt(0);
            TriggerEvent(EventQueue[0]);
            IsEventRunning = true;
            // Debug.Log($"Queue length = {EventQueue.Count}");
        }
        else
        {
            Debug.LogWarning("There is no upcoming event in the event queue.");
        }
    }

    /// <summary>
    /// Plays the event if the EventQueue is free, if not then it adds the event to the queue.
    /// </summary>
    /// <param name="eventId">The Id that is refering to a specific in-game event.</param>
    public void PlayEvent(string eventId)
    {
        AddEventToQueue(eventId);
        Debug.Log($"Added event by the id of {eventId}.");
    }
}
