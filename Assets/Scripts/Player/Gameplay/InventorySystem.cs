using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public string heldItem = "";
    public bool handsFree = true;
    public int equippedItemID = 0;

    public void EquipItem(string item, int id)
    {
        if (handsFree)
        {
            Debug.Log($"You've picked up, {item}!");
            heldItem = item;
            handsFree = false;
            equippedItemID = id;
        }
        else
        {
            Debug.Log($"Your hands are full with, {item}!");
        }
    }

    public void DequipItem(string item, int id)
    {
        if (item != heldItem)
        {
            Debug.LogWarning($"Wrong item was added to the inventory. Expected item value = {heldItem}");
        }
        else
        {
            heldItem = "";
            handsFree = true;
            equippedItemID = 0;
        }
    }

    public void ClearItemInHand()
    {
        if (handsFree)
        {
            Debug.LogWarning($"There is no item in hand.");
        }
        else
        {
            heldItem = "";
            handsFree = true;
            equippedItemID = 0;
        }
    }

    public bool IsHoldingItem()
    {
        if (heldItem == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
