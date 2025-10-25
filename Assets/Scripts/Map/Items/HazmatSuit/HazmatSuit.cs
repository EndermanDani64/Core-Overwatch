using UnityEngine;

public class HazmatSuit : MonoBehaviour
{
    [SerializeField] private Health health;

    public static bool isHazmat = false;

    /// <summary>
    /// Equips the hazmat suit if the player doesn't wear it already.
    /// </summary>
    public void WearSuit()
    {
        if (isHazmat)
        {
            Debug.LogWarning("Player is already wearing a suit.");
        }
        else
        {
            isHazmat = true;
            gameObject.transform.position = new Vector3(500, 500, 500);
        }
    }

    /// <summary>
    /// Returns wheter the player wears a suit or not. (return value: bool)
    /// </summary>
    public bool IsWearingSuit()
    {
        if (isHazmat)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
