using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupplyDeposit : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private ShoutSystem shoutSystem;

    [SerializeField] private TMP_Text outputText;

    private List<AlarmPanel> allAlarms = new List<AlarmPanel>();

    public static int supplyedValue = 100;

    private void Awake()
    {
        allAlarms.AddRange(Object.FindObjectsByType<AlarmPanel>(FindObjectsSortMode.None));
    }

    public void DecreaseSupplyValue(int decreaseValue)
    {
        int checkValue = supplyedValue - decreaseValue;
        if (checkValue > 0)
        {
            supplyedValue -= decreaseValue;
            outputText.text = $"Supply: {supplyedValue}%";

            foreach (AlarmPanel alarmPanel in allAlarms)
            {
                alarmPanel.StopAlarm();
            }
        }
        else
        {
            outputText.text = "Supply: 0%";
            if (!shoutSystem.IsActive())
            {
                shoutSystem.ShowMessage("Warning: ECoolant is depleted of supply.");
                foreach (var alarmPanel in allAlarms)
                {
                    alarmPanel.StopAlarm();
                    alarmPanel.StartAlarm_ECoolantDepleted();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("input detected");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("ray collided");
                if (hit.collider.CompareTag("ECoolantSupplyDeposit"))
                {
                    //valueStorage.ReleaseID(inventorySystem.equippedItemID);
                    if (inventorySystem.heldItem == "ECoolantSupply")
                    {
                        int checkValue = supplyedValue + ValueStorage.ECOOLANT_SUPPLY_ADD;
                        if (checkValue > 100)
                        {
                            supplyedValue = 100;
                            outputText.text = $"Supply: 100%";
                        }
                        else
                        {
                            supplyedValue += ValueStorage.ECOOLANT_SUPPLY_ADD;
                            outputText.text = $"Supply: {supplyedValue}%";
                        }
                        inventorySystem.ClearItemInHand();
                        SupplyCreation.isSupplyExists = false;
                        Debug.Log($"Inv cleared. {inventorySystem.heldItem}");
                    }
                }
            }
        }
    }
}
