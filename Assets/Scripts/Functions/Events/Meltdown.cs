using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class Meltdown : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private TempController tempController;
    [SerializeField] private PressureControl PressureControl;
    [SerializeField] private BlackoutEvent BlackoutEvent;
    [SerializeField] private CoreEffects CoreEffects;
    [SerializeField] private ECoolantController ECoolantController;
    [SerializeField] private ShoutSystem ShoutSystem;
    [SerializeField] private BlastDoorController BlastDoorController;

    [Header("Event materials")]
    [SerializeField] private SoundSystem SoundSystem;
    private List<AlarmPanel> allAlarms = new List<AlarmPanel>();
    [SerializeField] private Timer timer;

    [Header("Audios")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip meltdownEventFirst;
    [SerializeField] private AudioClip meltdownEventSecond;
    //[SerializeField] private AudioClip successECoolant;

    [Header("VFX effects")]
    [SerializeField] private ParticleSystem CoreRadioation;
    [SerializeField] private ParticleSystem Steam1;
    [SerializeField] private ParticleSystem Steam2;

    //[Header("Animations")]
    public Animator animator;

    private void Awake()
    {
        allAlarms.AddRange(Object.FindObjectsByType<AlarmPanel>(FindObjectsSortMode.None));
    }

    private void Start()
    {
        StartCoroutine(CheckForMeltdownEvent());
    }

    private IEnumerator CheckForMeltdownEvent()
    {
        while (OverallEvents.IsMainEventRunning == false) // || !TempController.isMeltdown
        {
            /*Debug.LogWarning($"tempController.isMeltdown = {tempController.isMeltdown}");
            Debug.LogWarning($"OverallEvents.IsEventRunning = {OverallEvents.IsEventRunning}");*/
            if (tempController.isMeltdown && OverallEvents.IsEventRunning == false)
            {
                StartCoroutine(MeltdownEvent());
                yield break;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator CoreShockWaves()
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

    private IEnumerator MeltdownEvent()
    {   
        OverallEvents.IsMainEventRunning = true;
        Debug.Log($"MeltdownEvent started. | TempController.isMeltdown = {tempController.isMeltdown}");
        StartCoroutine(CoreShockWaves());
        timer.StartTimer();
        SoundSystem.BackgroundSoundsMute();
        foreach (var alarmPanel in allAlarms)
        {
            alarmPanel.StartAlarm_Meltdown();
        }
        source.PlayOneShot(meltdownEventFirst);
        yield return new WaitForSeconds(83);
        ShoutSystem.ShowMessage("E-Coolant is now avalible.");
        ECoolantController.StartIEnumerator();
        Debug.Log("ECoolant Started. + message should have shown");
        yield return new WaitForSeconds(94.01f);

        if (ECoolantController.isECoolantSucces)
        {
            Debug.Log("ECoolant was successful.");
            ShoutSystem.HideMessage();
            StopMeltdown();
        }
        else
        {
            var emission = CoreRadioation.emission;
            emission.enabled = true;
            Debug.Log("Meltdown continues, no successful ECoolant.");
            ShoutSystem.ShowMessage("E-Coolant failed!");
            source.PlayOneShot(meltdownEventSecond);

            yield return new WaitForSeconds(0.85f);
            var steam1Emission = Steam1.emission;
            steam1Emission.rateOverTime = 150;

            var steam2Emission = Steam2.emission;
            steam2Emission.rateOverTime = 150;
            steam2Emission.enabled = true;

            yield return new WaitForSeconds(2.5f);
            ShoutSystem.HideMessage();

            yield return new WaitForSeconds(90f);
            BlastDoorController.ShutDownBlastDoors();

            yield return new WaitForSeconds(12);
            BlackoutEvent.ForceBlackout();

            yield return new WaitForSeconds(521.65f);
            Debug.Log("Loading MainMenuScene...");
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void StopMeltdown()
    {
        TempController.temp = 0; // 3000
        tempController.isError = false;
        PressureControl.isPressurized = false;
        PressureControl.isError = false;
        PressureControl.pressure = 250;
        foreach (var alarmPanel in allAlarms)
        {
            alarmPanel.StopAlarm();
        }
        StopCoroutine(CoreShockWaves());
        StopCoroutine(MeltdownEvent());
        //source.PlayOneShot(successECoolant);
        StartCoroutine(CheckForMeltdownEvent());
        StartCoroutine(CooldownAfterMeltdownECoolantSuccess());
    }

    public bool DEV_ForceMeltdown()
    {
        tempController.isMeltdown = true;
        StartCoroutine(MeltdownEvent());
        return true;
    }

    IEnumerator CooldownAfterMeltdownECoolantSuccess()
    {
        yield return new WaitForSeconds(102);
        OverallEvents.IsMainEventRunning = false;
        tempController.isMeltdown = false;
    }
}
