using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SupplyCreation : MonoBehaviour
{
    [SerializeField] private ElectricityManagger electricityManagger;
    [SerializeField] private Button creationButton;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform supplyTransform;

    public static bool isSupplyExists = false;

    /*private void Start()
    {
        spawnPoint.position = ;
    }*/

    public void ButtonActivation()
    {
        StartCoroutine(CreationManagger());
    }

    public void SupplyCreate()
    {
        if (ElectricityManagger.electricity - ValueStorage.ELECTRICITY_ECOOLANT_SUPPLY_CREATION_COST >= 0 && !isSupplyExists)
        {
            supplyTransform.position = new Vector3(-17.98f, 17.97f, -24.95f);
            isSupplyExists = true;
            ElectricityManagger.electricity -= ValueStorage.ELECTRICITY_ECOOLANT_SUPPLY_CREATION_COST; 
            Debug.Log("SupplyCreate has been spawned.");
        }
    }

    public IEnumerator CreationManagger()
    {
        if (creationButton.interactable)
        {
            creationButton.interactable = false;
            SupplyCreate();
            yield return new WaitForSeconds(5);
            Debug.Log("SupplyCreate()");
            yield return new WaitForSeconds(2);
            creationButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("WARNING!");
        }
    }
}
