using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIRaycast : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform eCoolantSupplyGameObject;
    [SerializeField] private InventorySystem inventorySystem;

    //[SerializeField] GameObject[] eCoolantSupplyCreates = { };
    [SerializeField] private TMP_Text pickupText;

    [SerializeField] private int minDistanceToGrab = 4;

    private ItemIDStore equippedID = null;
    private ECoolantSupplyCreate equippedECoolantSupplyCreate = null;
    private Transform equippedTransform = null;
    Rigidbody equippedRigidbody = null;
    void Update()
    {
        if (OverlayUIManager.isPaused)
            return;

        // Innen kezdõdik minden logika, de csak ha nincs pause
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit.collider.gameObject.GetComponent<Button>() != null)
                    {
                        ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                }
            }
        }

        Ray rayy = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitt;

        if (Physics.Raycast(rayy, out hitt))
        {
            if (hitt.collider.CompareTag("ECoolantSupply"))
            {
                if (!pickupText.enabled)
                {
                    pickupText.enabled = true;
                    pickupText.text = "Pick up [E]";
                }
            }
            else if (hitt.collider.CompareTag("ECoolantSupplyDeposit"))
            {
                if (!pickupText.enabled)
                {
                    pickupText.enabled = true;
                    pickupText.text = "Deposit [E]";
                }
            }
            else
            {
                pickupText.enabled = false;
            }
        }

        // INVENTORY SYSTEM

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ECoolantSupply"))
                {
                    float distance = Vector3.Distance(playerTransform.position, hit.collider.transform.position);

                    if (distance <= minDistanceToGrab)
                    {
                        Transform transform = hit.collider.transform; ;
                        ItemIDStore currentID = hit.collider.GetComponent<ItemIDStore>();
                        ECoolantSupplyCreate eCoolantSupplyCreate = hit.collider.GetComponent<ECoolantSupplyCreate>();

                        if (currentID != null && eCoolantSupplyCreate != null)
                        {
                            Rigidbody rigidbody = hit.collider.GetComponent<Rigidbody>();
                            equippedRigidbody = hit.collider.GetComponent<Rigidbody>();

                            eCoolantSupplyCreate.PickUp(hit.collider.transform, currentID.ID);
                            rigidbody.isKinematic = true;

                            equippedID = currentID;
                            equippedECoolantSupplyCreate = eCoolantSupplyCreate;
                            equippedTransform = transform;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!inventorySystem.handsFree && inventorySystem.heldItem == "ECoolantSupply")
            {
                /*if (equippedID != null && equippedECoolantSupplyCreate != null)
                {
                    equippedECoolantSupplyCreate.Drop(equippedTransform, equippedID.ID);
                    equippedRigidbody.isKinematic = false;
                }*/
                equippedECoolantSupplyCreate.Drop(equippedTransform, equippedID.ID);
                equippedRigidbody.isKinematic = false;
            }
        }
    }
}