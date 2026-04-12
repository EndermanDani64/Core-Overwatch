using UnityEngine;

public class ValueTransfer : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private GeneratorController generatorController;
    [SerializeField] private PlayerAudioEmitter soundSystem;
    [SerializeField] private FanOverwatch fanOverwatch;
    [SerializeField] private OverallEvents overallEvents;
    [SerializeField] private ShoutSystem shoutSystem;

    [Header("GameObjects")]
    [SerializeField] private AudioSource source;

    private void Start()
    {
        TempController.temp = PlayManagger.START_TEMP;
        PressureControl.pressure = PlayManagger.START_PS;
        ElectricityManagger.electricity = PlayManagger.START_ELECTRICITY;
        fanOverwatch.Transfer_ForceResetAll();
    }

    public void RessetValues_Default()
    {
        TempController.temp = PlayManagger.START_TEMP;
        PressureControl.pressure = PlayManagger.START_PS;
        ElectricityManagger.electricity = PlayManagger.START_ELECTRICITY;
        fanOverwatch.Transfer_ForceResetAll();
    }
}
