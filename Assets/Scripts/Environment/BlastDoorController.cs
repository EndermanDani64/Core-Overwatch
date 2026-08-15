using UnityEngine;
using UnityEngine.UI;

public class BlastDoorController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private BlastDoor[] doors;
    [SerializeField] private UnityEngine.UI.Button ForceButton;

    private void Start()
    {
        doors = Object.FindObjectsByType<BlastDoor>(FindObjectsSortMode.None);
    }

    public void ShutDownBlastDoors()
    {
        foreach (BlastDoor door in doors)
        {
            door.ToggleDoor();
        }
        ForceButton.interactable = false;
    }
}
    