using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerInteractionsManager : MonoBehaviour
{
    void Update()
    {
        if (OverlayUIManager.isPaused) { return; }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        CheckFor_UIButton(ray);
        CheckFor_TargetLabels(ray);
        CheckFor_ElevatorButton(ray);
        CheckFor_ItemInteraction(ray);
        CheckFor_HazmatSuit(ray);

        CheckFor_TabletInteraction();
    }

    private void CheckFor_UIButton(Ray ray)
    {
        if (Input.GetMouseButtonDown(0))
        {
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
    }

    private void CheckFor_TargetLabels(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("ECoolantSupply"))
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Pick up [E]";
                }
            }
            else if (hit.collider.CompareTag("ECoolantSupplyDeposit"))
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Deposit [E]";
                }
            }
            else if (hit.collider.CompareTag("Fixable") && Fixables.isFixableAvalible)
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Fix [hold: E]";
                }
            }
            else if (hit.collider.CompareTag("FixablePipe") && !hit.collider.GetComponent<OverflowEvent_FixablePipe>().isFixed)
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Fix [hold: E]";
                }
            }
            else if (hit.collider.CompareTag("HazmatSuit"))
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Equip [hold: E]";
                }
            }
            else if (hit.collider.CompareTag("ElevatorButton"))
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Call [Left Click]";
                }
            }
            else if (hit.collider.CompareTag("ElevatorButton_Nav"))
            {
                if (!targetLabel.enabled)
                {
                    targetLabel.enabled = true;
                    targetLabel.text = "Call [Left Click]";
                }
            }
            else
            {
                targetLabel.enabled = false;
            }
        }
    }

    private void CheckFor_ItemInteraction(Ray ray)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ECoolantSupply"))
                {
                    float distance = Vector3.Distance(_playerTransform.position, hit.collider.transform.position);

                    if (distance <= _minDisToGrab)
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

                            _equippedID = currentID;
                            _equippedECoolantSupplyCreate = eCoolantSupplyCreate;
                            _equippedTransform = transform;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!Player.handsFree && _inventorySystem.heldItem == "ECoolantSupply")
            {
                /*if (equippedID != null && equippedECoolantSupplyCreate != null)
                {
                    equippedECoolantSupplyCreate.Drop(equippedTransform, equippedID.ID);
                    equippedRigidbody.isKinematic = false;
                }*/
                _equippedECoolantSupplyCreate.Drop(_equippedTransform, _equippedID.ID);
                equippedRigidbody.isKinematic = false;
            }
        }
    }

    private void CheckFor_HazmatSuit(Ray ray)
    {
        if (Input.GetKey(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("HazmatSuit"))
                {
                    float distance = Vector3.Distance(_playerTransform.position, hit.collider.transform.position);

                    if (distance <= _minDisToGrab && _interactionHoldTime < ValueStorage.ITEM_HAZMATSUIT_PICKUPTIME) // if the player reaches but didn't hold the button enough
                    {
                        _interactionHoldTime += Time.deltaTime;
                        Debug.Log($"timeHeld: {Mathf.Round(_interactionHoldTime)}");
                    }
                    else if (distance <= _minDisToGrab && _interactionHoldTime >= ValueStorage.ITEM_HAZMATSUIT_PICKUPTIME) // if the player has been holding the button for ValueStorage.ITEM_HAZMATSUIT_PICKUPTIME
                    {
                        _hazmatSuit.WearSuit();
                    }
                }
            }
        }
    }

    private void CheckFor_ElevatorButton(Ray ray)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject targetGameObject = hit.collider.gameObject;

                if (targetGameObject.tag == "ElevatorButton")
                {
                    targetGameObject.GetComponentInChildren<ElevatorButton>().CallToFloor(targetGameObject.GetComponentInChildren<ElevatorButton>().localFloor);
                }
                else if (targetGameObject.tag == "ElevatorButton_Nav")
                {
                    targetGameObject.GetComponentInChildren<ElevatorButton>().GoToFloor();
                }
            }
        }
    }
    private void CheckFor_TabletInteraction()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            StartCoroutine(_tablet.OpenCloseTablet());
        }
    }

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _eCoolantSupplyGameObject;

    [SerializeField] private Tablet _tablet;

    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private HazmatSuit _hazmatSuit;

    [SerializeField] private TMP_Text targetLabel;

    [SerializeField] private int _minDisToGrab = 4;
    private float _interactionHoldTime = 0f;

    private ItemIDStore _equippedID;
    private ECoolantSupplyCreate _equippedECoolantSupplyCreate;
    private Transform _equippedTransform;
    Rigidbody equippedRigidbody;
}