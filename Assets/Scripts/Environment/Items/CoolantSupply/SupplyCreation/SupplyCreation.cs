using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SupplyCreation : MonoBehaviour
{
    [SerializeField] private EnergyManagger electricityManagger;
    [SerializeField] private UnityEngine.UI.Button creationButton;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform supplyTransform;

    public static bool isSupplyExists = false;

    public void ButtonActivation()
    {
        StartCoroutine(CreationManagger());
    }

    public void SupplyCreate()
    {
        if (EnergyManagger.MasterBatteryData.Energy - ValueStorage.ENERGY_CONSUMPTION_COOLANT_CREATION >= 0 && !isSupplyExists)
        {
            supplyTransform.position = new Vector3(-17.98f, 17.97f, -24.95f);
            isSupplyExists = true;
            EnergyManagger.electricity -= ValueStorage.ENERGY_CONSUMPTION_COOLANT_CREATION; 
            Debug.Log("SupplyCreate has been spawned.");
        }
    }

    public IEnumerator CreationManagger()
    {
        if (creationButton.interactable)
        {
            creationButton.interactable = false;
            SupplyCreate();
            yield return new WaitForSeconds(7);
            creationButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("WARNING!");
        }
    }
}
