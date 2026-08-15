using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ShoutSystem : MonoBehaviour
{
    public Image ShoutUIOverall;
    public TMP_Text ShoutText;

    private Coroutine _currentMessage;
    
    public void SendMessage(string message, float showTime = 5f, float delay = 0f)
    {
        _currentMessage = StartCoroutine(SendOutTimedShoutMessage(message, showTime, delay));
    }

    public void StopMessage()
    {
        StopCoroutine(_currentMessage);
    }

    public void ShowMessage(string message)
    {
        ShoutUIOverall.enabled = true;
        ShoutText.enabled = true;
        ShoutText.text = message;
    }

    public void HideMessage()
    {
        ShoutUIOverall.enabled = false;
        ShoutText.enabled = false;
    }

    public bool IsActive()
    {
        return ShoutUIOverall.enabled;
    }


    // submethods //

    private IEnumerator SendOutTimedShoutMessage(string message, float showTime, float delay)
    {
        yield return new WaitForSeconds(delay);

        ShowMessage(message);

        yield return new WaitForSeconds(showTime);

        HideMessage();
    }
}