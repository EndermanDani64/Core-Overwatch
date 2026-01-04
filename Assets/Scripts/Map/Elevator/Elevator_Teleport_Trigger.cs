using UnityEngine;

public class Elevator_Teleport_Trigger : MonoBehaviour
{
    [SerializeField] private ElevatorManager manager;

    [SerializeField] private Transform elevatorBody;
    public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.isPlayerIn = true;
            playerTransform.SetParent(elevatorBody);
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.isPlayerIn = false;
            playerTransform.SetParent(null);
            playerTransform = null;
        }
    }   
}
