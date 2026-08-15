using UnityEngine;

public class FacilityDoorManager : MonoBehaviour
{

    [SerializeField] private GameObject[] manualDoorButtons = { };
    public bool _doorsEnabled = true;
    
    
    public void DisableManualDoors()
    {
        if (_doorsEnabled)
        {
            foreach (GameObject obj in manualDoorButtons)
            {
                obj.GetComponent<ButtonManager>().Enabled = false;
            }
            _doorsEnabled = !_doorsEnabled;
            EnergyManagger.MasterBatteryData.EnergyConsumption -= ValueStorage.ENERGY_CONSUMPTION_MANUALDOORS;
        }
    }

    public void EnableManualDoors()
    {
        if (!_doorsEnabled)
        {
            foreach (GameObject obj in manualDoorButtons)
            {
                ButtonManager button = obj.GetComponent<ButtonManager>();
                button.Enabled = true;
            }
            _doorsEnabled = !_doorsEnabled;
            EnergyManagger.MasterBatteryData.EnergyConsumption += ValueStorage.ENERGY_CONSUMPTION_MANUALDOORS;
        }
    }


    // -- Built-in -- //

    private void Start()
    {
        manualDoorButtons = GameObject.FindGameObjectsWithTag("ManualDoorButton");
        EnergyManagger.MasterBatteryData.EnergyConsumption += ValueStorage.ENERGY_CONSUMPTION_MANUALDOORS;
    }

}
