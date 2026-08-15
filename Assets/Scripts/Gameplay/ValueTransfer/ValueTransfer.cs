using UnityEngine;

public class ValueTransfer : MonoBehaviour
{
    [Header("Important scripts")]
    [SerializeField] private GeneratorController generatorController;
    [SerializeField] private PlayerAudioEmitter soundSystem;
    [SerializeField] private FanOverwatch fanOverwatch;
    [SerializeField] private OverallEvents overallEvents;
    [SerializeField] private ShoutSystem shoutSystem;

    private void Start()
    {
        ReactorManager.ReactorData.Temperature = PlayManagger.START_TEMP;
        ReactorManager.ReactorData.Pressure = PlayManagger.START_PS;
        EnergyManagger.electricity = PlayManagger.START_ELECTRICITY;
        fanOverwatch.Transfer_ForceResetAll();
    }

    public void RessetValues_Default()
    {
        ReactorManager.ReactorData.Temperature = PlayManagger.START_TEMP;
        ReactorManager.ReactorData.Pressure = PlayManagger.START_PS;
        EnergyManagger.electricity = PlayManagger.START_ELECTRICITY;
        fanOverwatch.Transfer_ForceResetAll();
    }
}
