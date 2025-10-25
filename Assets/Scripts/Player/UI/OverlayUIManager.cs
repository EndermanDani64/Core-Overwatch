using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class OverlayUIManager : MonoBehaviour
{
    [SerializeField] private Canvas PauseMenuCanvas;
    [SerializeField] private Canvas DeveloperMenuCanvas;
    [SerializeField] private Canvas OptionsCanvas;
    [SerializeField] private Movement Player;
    [SerializeField] private Camera Camera;
    [SerializeField] private EventSystem EventSystem;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private InputSystemUIInputModule defaultInputModule;
    [SerializeField] private FPSInputModule fpsInputModule;

    [SerializeField] private Meltdown MeltdownEvent;
    [SerializeField] private TempController TempController;
    [SerializeField] private OptionsMenuManager optionsMenuManager;

    private float PreviusSens = 0f;
    public static bool isPaused = false;

    private void Start()
    {
        if (Application.isPlaying)
        {
            if (!EventSystem.enabled)
                EventSystem.enabled = true;

            if (!Camera.enabled)
                Camera.enabled = true;
        }

        PauseMenuCanvas.enabled = false;
        Time.timeScale = 1f;

        isPaused = false;

        StartCoroutine(LockCursorNextFrame());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) 
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isPaused)
            {
                OpenDeveloperMenu();
            }
            else
            {
                ResumeGame();
            }
        }

        if (isPaused)
        {
            ShowCursor();
        }
        else
        {
            HideCursor();
        }
        
        /*if (cooldownFrames > 0)
        {
            cooldownFrames -= 1;
            Debug.Log($"cooldownFrames = {cooldownFrames}");
        }*/
    }

    /// <summary>
    /// Resumes the game when paused by any UI properly.
    /// </summary>
    public void ResumeGame()
    {
        if (PauseMenuCanvas.enabled)
        {
            // ha a pause menu aktív akkor azt zárom be, hapedig a dev menü aktív akkor pedig azt
            PauseMenuCanvas.enabled = false;
            //Time.timeScale = 1f;
            //audioSource.Play(); EZ NEM JÓ HASZNÁLAT!!
            isPaused = false;

            HideCursor();

            fpsInputModule.enabled = true;
            defaultInputModule.enabled = false;
        }
        else if (DeveloperMenuCanvas.enabled)
        {
            DeveloperMenuCanvas.enabled = false;
            //Time.timeScale = 1f;
            //audioSource.Play(); EZ NEM JÓ HASZNÁLAT!!
            isPaused = false;

            HideCursor();

            fpsInputModule.enabled = true;
            defaultInputModule.enabled = false;
        }
    }

    /// <summary>
    /// Opens the Pause Menu UI properly.
    /// </summary>
    public void PauseGame()
    {
        if (!DeveloperMenuCanvas.enabled && !isPaused)
        {
            PauseMenuCanvas.enabled = true;
            //Time.timeScale = 0f;
            //audioSource.Pause(); 
            isPaused = true;

            ShowCursor();

            fpsInputModule.enabled = false;
            defaultInputModule.enabled = true;
        }
    }

    public void ToggleOptions()
    {
        if (!OptionsCanvas.enabled && isPaused)
        {
            OptionsCanvas.enabled = true;
            PauseMenuCanvas.enabled = false;
        }
        else if (OptionsCanvas.enabled && isPaused)
        {
            OptionsCanvas.enabled = false;
            optionsMenuManager.CloseAudioTab();
            optionsMenuManager.CloseGraphicsTab();
            PauseMenuCanvas.enabled = true;
        }
    }
    /// <summary>
    /// Opens the Developer Menu properly.
    /// </summary>
    public void OpenDeveloperMenu()
    {
        if (!PauseMenuCanvas.enabled && !isPaused)
        {
            DeveloperMenuCanvas.enabled = true;
            //Time.timeScale = 0f;
            //audioSource.Pause();
            isPaused = true;

            ShowCursor();

            fpsInputModule.enabled = false;
            defaultInputModule.enabled = true;
        }
    }

    /// <summary>
    /// Locks and then hides the cursor.
    /// </summary>
    public void HideCursor()
    {
        if (Cursor.visible == true && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// Unlocks and shows the cursor.
    /// </summary>
    public void ShowCursor()
    {
        if (Cursor.visible == false && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private IEnumerator LockCursorNextFrame()
    {
        yield return null; // várj 1 frame-et

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Debug.Log("Cursor locked on Start()");
    }

    /// <summary>
    /// (UNSTABLE DESCRIPTION) Switches the active Scene to the Scene that has the targetedSceneID set. 
    /// </summary>
    public void ReturnToLobby()
    {
        if (LoadingManagger.targetedSceneID == 1)
        {
            //MeltdownEvent.StopAllCoroutines();
            MeltdownEvent.StopMeltdown();
            PauseMenuCanvas.gameObject.SetActive(false);
            Player.Speed = 12;
            Player.mouseSensitivity = PreviusSens;
            EventSystem.enabled = false;
            Camera.enabled = false;
            if (ScoreManager.score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", ScoreManager.score);
                Debug.Log("Saved your high score!");
            }
            SceneManager.LoadScene("MainMenuScene");
        }
        else if (LoadingManagger.targetedSceneID == -1)
        {
            PauseMenuCanvas.gameObject.SetActive(false);
            Player.Speed = 12;
            Player.mouseSensitivity = PreviusSens;
            EventSystem.enabled = false;
            Camera.enabled = false;
            SceneManager.LoadScene("MainMenuScene");
        }
        else
        {
            Debug.LogWarning("targetedSceneID not set correctly.");
        }
    }
}