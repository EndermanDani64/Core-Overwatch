using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public class Meltdown : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private TempController _tempController;
    [SerializeField] private PressureControl PressureControl;
    [SerializeField] private BlackoutEvent BlackoutEvent;
    [SerializeField] private CoreEffects CoreEffects;
    [SerializeField] private ECoolantController ECoolantController;
    [SerializeField] private ShoutSystem _shoutSystem;
    [SerializeField] private BlastDoorController _blastDoorController;
    [SerializeField] private OverallEvents _overallEvents;
    [SerializeField] private PlayerAudioEmitter _soundSystem;

    [Header("Event materials")]
    private List<AlarmPanel> allAlarms = new List<AlarmPanel>();
    [SerializeField] private Timer timer;

    [Header("VFX effects")]
    [SerializeField] private ParticleSystem CoreRadioation;
    [SerializeField] private ParticleSystem Steam1;
    [SerializeField] private ParticleSystem Steam2;

    public Animator animator;

    private void Awake()
    {
        allAlarms.AddRange(Object.FindObjectsByType<AlarmPanel>(FindObjectsSortMode.None));
    }

    IEnumerator CoreShockWaves()
    {
        yield return new WaitForSeconds(313f);
        animator.Play("CoreShockwave", 0, 0f);
        //StartCoroutine(CoreEffects.CoreShock());
        yield return new WaitForSeconds(36f);
        animator.Play("CoreShockwave", 0, 0f);
        CoreEffects.StopAllCoroutines();
        animator.Play("CoreShockwave", 0, 0f);
        //StartCoroutine(CoreEffects.CoreShock());
        yield return new WaitForSeconds(52f);
        //CoreEffects.StopAllCoroutines();
        animator.Play("CoreShockwave", 0, 0f);
        //StartCoroutine(CoreEffects.CoreShock());
        yield return new WaitForSeconds(30f);
        //CoreEffects.StopAllCoroutines();
        animator.Play("CoreShockwave", 0, 0f);
        //StartCoroutine(CoreEffects.CoreShock());
    }

    IEnumerator MeltdownEvent_FirstSegment()
    {
        OverallEvents.IsMainEventRunning = true;

        StartCoroutine(CoreShockWaves());
        timer.StartTimer();
        _soundSystem.StopSounds();
        foreach (var alarmPanel in allAlarms)
        {
            alarmPanel.StartAlarm_Meltdown();
        }
        _soundSystem.PlaySound("ms_beforemeltdown");
        yield return new WaitForSeconds(83);
        _shoutSystem.ShowMessage("E-Coolant is now avalible.");
        ECoolantController.StartIEnumerator();
        Debug.Log("ECoolant Started. + message should have shown");
        yield return new WaitForSeconds(94.01f);

        _overallEvents.PlayEvent("meltdown_second");
    }

    IEnumerator MeltdownEvent_SecondSegment()
    {
        if (ECoolantController.isECoolantSucces)
        {
            Debug.Log("ECoolant was successful.");
            _shoutSystem.HideMessage();
            StopMeltdown();
        }
        else
        {
            var emission = CoreRadioation.emission;
            emission.enabled = true;
            Debug.Log("Meltdown continues, no successful ECoolant.");
            _shoutSystem.ShowMessage("E-Coolant failed!");

            yield return new WaitForSeconds(0.85f);
            var steam1Emission = Steam1.emission;
            steam1Emission.rateOverTime = 150;

            var steam2Emission = Steam2.emission;
            steam2Emission.rateOverTime = 150;
            steam2Emission.enabled = true;

            yield return new WaitForSeconds(2.5f);
            _shoutSystem.HideMessage();

            yield return new WaitForSeconds(90f);
            _blastDoorController.ShutDownBlastDoors();

            yield return new WaitForSeconds(12);
            BlackoutEvent.ForceBlackout();

            yield return new WaitForSeconds(521.65f);
            StopMeltdown();

            Debug.Log("Loading MainMenuScene...");
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void ForceMeltdown_FirstSegment()
    {
        OverallEvents.IsMeltdown = true;
        StartCoroutine(MeltdownEvent_FirstSegment());
    }
    public void ForceMeltdown_SecondSegment()
    {
        OverallEvents.IsMeltdown = true;
        StartCoroutine(MeltdownEvent_SecondSegment());
    }

    public void StopMeltdown()
    {
        ReactorManager.Temp = 0;
        _tempController.isError = false;
        PressureControl.isPressurized = false;
        PressureControl.isError = false;
        PressureControl.pressure = 250;

        foreach (var alarmPanel in allAlarms)
        {
            alarmPanel.StopAlarm();
        }

        StopCoroutine(CoreShockWaves());
        StopCoroutine(MeltdownEvent_FirstSegment());
        StopCoroutine(MeltdownEvent_SecondSegment());
        
        StartCoroutine(CooldownAfterMeltdownECoolantSuccess());
    }

    /// <summary>
    /// Returns with a true value if the event ran.
    /// </summary> 
    public bool DEV_ForceMeltdown()
    {
        OverallEvents.IsMeltdown = true;
        StartCoroutine(MeltdownEvent_FirstSegment());
        return true;
    }

    IEnumerator CooldownAfterMeltdownECoolantSuccess()
    {
        yield return new WaitForSeconds(102);
        OverallEvents.IsMainEventRunning = false;
        OverallEvents.IsMeltdown = false;
    }
}
