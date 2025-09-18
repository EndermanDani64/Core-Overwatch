using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayManagger : MonoBehaviour
{
    [SerializeField] private EventSystem EventSystem;
    private void Start()
    {
        /*if (Application.isPlaying)
        {
            if (!EventSystem.enabled)
            {
                EventSystem.enabled = true;
            }
        }*/
    }
    [SerializeField] private TMP_Text ButtonText;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Canvas StageSelectionCanvas;
    [SerializeField] private Canvas CustomLevelCanvas;
    public void ForceStartGame()
    {
        LoadingManagger.targetedSceneID = 1;
        ButtonText.text = "Loading...";
        EventSystem.enabled = false;
        SceneManager.LoadScene("LoadingScene");
    }

    public void ShowStageSelection()
    {
        PlayButton.interactable = false;
        OptionsButton.interactable = false;
        QuitButton.interactable = false;

        StageSelectionCanvas.enabled = true;
    }

    public void HideStageSelection()
    {
        PlayButton.interactable = true;
        OptionsButton.interactable = true;
        QuitButton.interactable = true;

        StageSelectionCanvas.enabled = false;
    }
    public void ShowCustomLevelCanvas()
    {
        CustomLevelCanvas.enabled = true;
        StageSelectionCanvas.enabled = false;
    }

    public void HideCustomLevelCanvas()
    {
        CustomLevelCanvas.enabled = false;
        StageSelectionCanvas.enabled = true;
    }

    public static bool START_ISONLINE = false;
    public static int START_TEMP = 0;
    public static int START_PS = 0;
    public static int START_ELECTRICITY = 0;

    //public static int START_CUSTOM_MAX_ELECTRICITY = ValueStorage.

    public void LoadLevel_test1()
    {
        LoadingManagger.targetedSceneID = 1;
        START_TEMP =  256;
        START_ELECTRICITY = 0;
        ValueStorage.ResetValues();
        SceneManager.LoadScene("LoadingScene");
    }

    public void LoadLevel_dev()
    {
        LoadingManagger.targetedSceneID = -1;
        ValueStorage.ResetValues();
        SceneManager.LoadScene("LoadingScene");
    }

    public void LoadLevel_ecoolant()
    {
        LoadingManagger.targetedSceneID = 1;
        ValueStorage.ResetValues();
        START_TEMP = 4500;
        START_PS = 100;
        START_ISONLINE = true;
        SceneManager.LoadScene("LoadingScene");
    }


    [SerializeField] private TMP_Dropdown reactorType;
    [SerializeField] private TMP_InputField maxElectricity;
    [SerializeField] private TMP_InputField ecoolantSuplyDecrease;
    [SerializeField] private TMP_InputField ecoolantSuplyValue;
    public static int selectedReactorType = 0;
    public void LoadLevel_custom()
    {
        LoadingManagger.targetedSceneID = 1; // in the future if there will be more maps we can choose this as well

        if (reactorType.value == 0)
        {
            selectedReactorType = 0;
            ValueStorage.REACTOR_TMP_DELTA = 2.5f;
        }
        else if (reactorType.value == 1)
        {
            selectedReactorType = 1;
            ValueStorage.REACTOR_TMP_DELTA = 2.5f;
        }
        else if (reactorType.value == 2)
        {
            selectedReactorType = 2;
            ValueStorage.REACTOR_TMP_DELTA = 1.5f;
        }

        if (maxElectricity.text != "" || int.Parse(maxElectricity.text) != 0) // if the electricity is not set or zero or it has something in it and its not = to zero, then
        {
            ValueStorage.ELECTRICITY_MAX = int.Parse(maxElectricity.text);
        }

        if (ecoolantSuplyDecrease.text != "" || int.Parse(ecoolantSuplyDecrease.text) != 0)
        {
            ValueStorage.COOLANT_SUPPLY_DECREASE = int.Parse(ecoolantSuplyDecrease.text);
        }

        if (ecoolantSuplyValue.text != "" || int.Parse(ecoolantSuplyValue.text) != 0)
        {
            ValueStorage.COOLANT_SUPPLY_ADD = int.Parse(ecoolantSuplyValue.text);
        }

        Debug.Log("Phase 2 complete");

        SceneManager.LoadScene("LoadingScene");
    }

}
    