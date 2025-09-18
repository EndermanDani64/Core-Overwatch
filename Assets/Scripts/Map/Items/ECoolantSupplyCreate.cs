using Unity.VisualScripting;
using UnityEngine;

public class ECoolantSupplyCreate : MonoBehaviour
{
    [SerializeField] InventorySystem inventorySystem;
    //[SerializeField] Transform eCoolantSupplyGameObject;
    [SerializeField] Transform playerTransform;

    Vector3 tempPlace = new Vector3(300, 300, 300);

    public void PickUp(Transform transform, int ID)
    {
        transform.position = tempPlace;
        inventorySystem.EquipItem("ECoolantSupply", ID);
    }

    public void Drop(Transform transform, int ID)
    {
        transform.position = playerTransform.position + playerTransform.forward;
        inventorySystem.DequipItem("ECoolantSupply", ID);
    }
}
