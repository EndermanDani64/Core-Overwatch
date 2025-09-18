using UnityEngine;
using TMPro;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private TMP_Text ButtonText;
    public void QuitGame()
    {
        ButtonText.text = "Quitting...";
        Application.Quit();
        Debug.Log("Quited from game.");
    }
}
