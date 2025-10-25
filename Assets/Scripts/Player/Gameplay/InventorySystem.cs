using UnityEngine;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public string heldItem = "";
    public string wornSuit = "";

    public bool handsFree = true;
    public bool naked = true;

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

    public void WearSuit(string suit)
    {
        if (naked)
        {
            Debug.Log($"You've put on, {suit}");
            naked = false;
            wornSuit = suit;
        }
        else
        {
            Debug.Log($"You are already wearing, {suit}!");
        }
    }

    public void UnequipSuit(string suit)
    {
        if (suit != wornSuit)
        {
            Debug.LogWarning($"Wrong suit was tried to unequip. Expected suit value = {wornSuit}");
            
        }
        else
        {
            wornSuit = "";
            naked = true;
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
