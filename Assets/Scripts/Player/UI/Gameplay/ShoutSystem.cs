using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShoutSystem : MonoBehaviour
{
    public Image ShoutUIOverall;
    public TMP_Text ShoutText;
    
    public void ShowMessage(string message)
    {
        ShoutUIOverall.enabled = true;
        ShoutText.enabled = true;
        ShoutText.text = message;
        //Debug.Log("Message shown.");
    }

    public void HideMessage()
    {
        ShoutUIOverall.enabled = false;
        ShoutText.enabled = false;
        //Debug.Log("Message hidden.");
    }

    public bool IsActive()
    {
        return ShoutUIOverall.enabled;
    }
}