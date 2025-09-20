using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GeneratorController : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private TempController TempController;
    [SerializeField] private SoundSystem SoundSystem;
    [SerializeField] private PressureControl pressureControl;
    [SerializeField] private FanOverwatch FanOverwatch;
    [SerializeField] private OverallEvents OverallEvents;
    [SerializeField] private ShoutSystem ShoutSystem;
    [SerializeField] private ElectricityManagger ElectricityManagger;
    [SerializeField] private BlackoutEvent blackoutEvent;
    [SerializeField] private ValueStorage ValueStorage;

    [Header("Generator UIs")]
    public Button StartStopButton;
    public TMP_Text GeneratorStatus;

    [Header("Generator variables")]
    [SerializeField] public static bool isGeneratorOnline = true;
    [SerializeField] public bool isGeneratorDestroyed = false;

    public void ToggleGeneratorStatus()
    {
        if (!isGeneratorDestroyed)
        {
            StartCoroutine(ButtonPressAndStartupDelay());
        }
    }

    public void ShutDown()
    {
        GeneratorStatus.text = "Offline";
        isGeneratorOnline = false;
    }

    private IEnumerator ButtonPressAndStartupDelay() // We press the on/off button
    {
        if (StartStopButton.enabled)
        {
            StartStopButton.interactable = false;

            if (!isGeneratorOnline && !isGeneratorDestroyed)
            {
                GeneratorStatus.text = "Starting up...";
                yield return new WaitForSeconds(5);
                GeneratorStatus.text = "Online";

                /*if (ChangeEnergyCoroutine != null) // if ChangeEnergy() Coroutine is not running, then
                {
                    ChangeEnergyCoroutine = null; // reset the Coroutine just for sure
                    ChangeEnergyCoroutine = StartCoroutine(ChangeEnergy());
                    Debug.Log($"GeneratorController (to online): Coroutine was null");
                }*/

                isGeneratorOnline = true;
                Debug.Log($"isGeneratorOnline: {isGeneratorOnline}");
            } 
            else if (isGeneratorOnline && !isGeneratorDestroyed) //  && !blackoutEvent.isBlackout
            {
                GeneratorStatus.text = "Shutting down...";
                yield return new WaitForSeconds(5);
                GeneratorStatus.text = "Offline";

                /*if (ChangeEnergyCoroutine == null)
                {
                    ChangeEnergyCoroutine = null;
                    ChangeEnergyCoroutine = StartCoroutine(ChangeEnergy());
                }
                else
                {
                    ChangeEnergyCoroutine = null;
                    Debug.Log($"GeneratorController (to offline): Coroutine was null");
                }*/

                isGeneratorOnline = false;
                //Debug.Log($"isGeneratorOnline: {isGeneratorOnline}");
            }
            /*else if (blackoutEvent.isBlackout)
            {
                StopAllCoroutines();
            }*/
            StartStopButton.interactable = true;
        }
    }

    private bool isMessageShown = false;
    private bool isDecresing = false;

    private IEnumerator SendOutShoutMessage(string message)
    {
        bool temp = false;
        while (isDecresing)
        {
            if (temp)
            {
                temp = true;
                yield return new WaitForSeconds(Random.Range(8, 25));
                temp = false;
            }
            if (!isMessageShown)
            {
                ShoutSystem.ShowMessage(message);
                isMessageShown = true;
            }
            yield return new WaitForSeconds(60);

            if (isMessageShown)
            {
                ShoutSystem.HideMessage();
                isMessageShown = false;
            }
        }
    }
    public void ChangeEnergy()
    {
        /*if (ElectricityManagger.isDepletedEnergy)
            {
                Debug.Log("Electricity depleted. Waiting...");
                if (isGeneratorOnline)
                {
                    ElectricityManagger.isDepletedEnergy = false;
                }
                yield return new WaitForSeconds(1);
                continue;
            }*/

        if (isGeneratorOnline)
        {
            if (ElectricityManagger.electricity <= ValueStorage.ELECTRICITY_MAX) // && ElectricityManagger.electricity != 0
            {
                ElectricityManagger.IncreaseEnergy(ValueStorage.ELECTRICITY_BASE_INCREASE_VALUE, ElectricityDecreaseValueManagger.decreaseValue);

                isDecresing = false;
            }
        }

        if (!isGeneratorOnline)
        {
            StartCoroutine(SendOutShoutMessage("Watch the decreasing energy levels."));
            ElectricityManagger.DecreaseElectricityOnOffline(ElectricityDecreaseValueManagger.decreaseValue);
            Debug.Log($"decreaseEnergy.decreaseValue = {ElectricityDecreaseValueManagger.decreaseValue}");

            if (ElectricityManagger.electricity <= 0)
            {
                isDecresing = false;
            }
            else
            {
                isDecresing = true;
            }
            //previousGeneratorStatus = false;
        }
    } 
}
