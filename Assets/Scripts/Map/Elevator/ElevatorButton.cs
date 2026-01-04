using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField] private int floor;
    [SerializeField] private int designatedFloor;

    [SerializeField] private ElevatorManager elevatorManager;

    /// <summary>
    /// Calls the ElevatorBody to the floor's valued floor.
    /// </summary>
    public void CallToFloor()
    {
        if (!elevatorManager.isMoving)
        {
            // Debug.Log("MoveToFloor(floor); - ElevatorButton.cs");
            StartCoroutine(elevatorManager.MoveToFloor(floor));
        }
    }

    /// <summary>
    /// Moves the ElevatorBody to the designatedFloor's valued floor.
    /// </summary>
    public void GoToFloor()
    {
        if (!elevatorManager.isMoving)
        {
            // Debug.Log("GoToFloor(); - ElevatorButton.cs");
            StartCoroutine(elevatorManager.MoveToFloor(designatedFloor));
        }
    }
}
