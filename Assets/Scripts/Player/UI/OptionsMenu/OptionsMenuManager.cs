using UnityEngine;
using TMPro;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas OptionsCanvas;
    [SerializeField] private Canvas AudioCanvas;
    [SerializeField] private Canvas GraphicsCanvas;

    public void OpenOptions()
    {
        OptionsCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        OptionsCanvas.enabled = false;
    }

    public void ToggleAudioTab()
    {
        if (!AudioCanvas.enabled)
        {
            GraphicsCanvas.enabled = false;
            AudioCanvas.enabled = true;
        }
        else
        {
            AudioCanvas.enabled = false;
        }
    }

    public void ToggleGraphicsTab()
    {
        if (!GraphicsCanvas.enabled)
        {
            AudioCanvas.enabled = false;
            GraphicsCanvas.enabled = true;
        }
        else
        {
            GraphicsCanvas.enabled = false;
        }
    }

    public void CloseAudioTab()
    {
        AudioCanvas.enabled = false;
    }

    public void CloseGraphicsTab()
    {
        GraphicsCanvas.enabled = false;
    }
}
