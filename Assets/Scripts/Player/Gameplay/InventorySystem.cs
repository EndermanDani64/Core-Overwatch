using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public string heldItem = "";
    public int equippedItemID = 0;

    /// <summary>
    /// Set's the equipped item to the item parameter.
    /// </summary>
    /// <param name="item">The item's code used specific name. Check ValueStorage for the possible names.</param>
    /// <param name="id">The item's id. If there is more items of this type then I use ids to distinct them.</param>
    public void EquipItem(string item, int id)
    {
        if (ValueStorage.VALID_ITEM_IDS.Contains(item))
        {
            if (Player.handsFree)
            {
                heldItem = item;
                equippedItemID = id;
                Player.SetHolding(item);
                Debug.Log($"You've picked up, {item}!");
            }
            else
            {
                Debug.Log($"Your hands are full with, {item}!");
            }
        }
        else Debug.LogWarning("The set item in EquipItem() is not valid.", gameObject);
    }

    /// <summary>
    /// Set's the equipped item to the item parameter.
    /// </summary>
    public void DequipItem()
    {
        if (heldItem != "")
        {
            heldItem = "";
            equippedItemID = 0;
            Player.SetHoldingToNone();
        }
    }

    /// <summary>
    /// Set's the worn suit to the item parameter.
    /// </summary>
    public void WearSuit(string suit)
    {
        if (!Player.naked)
        {
            Debug.LogWarning($"You are already wearing, {suit}!");
        }
        else
        {
            if (ValueStorage.VALID_WEARABLE_IDS.Contains(suit))
            {
                Player.SetWearing(suit);
                Debug.Log($"You've put on, {suit}");
            }
            else Debug.LogWarning("The set suit in SetWearing() is not valid.", gameObject);
        }
    }

    /// <summary>
    /// Set's the worn suit to naked.
    /// </summary>
    public void UnequipSuit(string suit)
    {
        if (!Player.naked) Player.SetWearingToNone();
        else Debug.LogWarning("The UnequipSuit() is called at a point where the Player.naked is true.", gameObject);
    }

    public void ClearItemInHand()
    {
        if (Player.handsFree)
        {
            Debug.LogWarning($"There is no item in hand.", gameObject);
        }
        else
        {
            heldItem = "";
            Player.SetHoldingToNone();
            equippedItemID = 0;
        }
    }

    public bool IsHoldingItem()
    {
        if (heldItem == null || heldItem == "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
