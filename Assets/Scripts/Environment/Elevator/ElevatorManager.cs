using System.Collections;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    private int _currentFloor = 0;
    public bool isMoving = false;

    public bool isPlayerIn = false;

    [SerializeField] private BoxCollider playerTrigger;
    [SerializeField] private Transform playerTransform;

    // doors
    [SerializeField] private Animator door_0_animator;
    private bool door_0_isOpen = false;
    [SerializeField] private Animator door_1_animator;
    private bool door_1_isOpen = false;
    [SerializeField] private Animator door_inside_animator;
    private bool door_inside_isOpen = false;

    [SerializeField] private Animator elevatorBody_animator;


    /// <summary>
    /// Moves the Elevator to the destination floor.
    /// </summary>
    public IEnumerator MoveToFloor(int destination)
    {
        if (destination < 0) { Debug.LogWarning("destination is negative."); yield return null; }

        if (_currentFloor < destination)
        {
            isMoving = true;
            CloseDoors_CurrentFloor();

            yield return new WaitForSeconds(1.5f);

            // play elevator moving sound
            elevatorBody_animator.Play("ElevatorMoveUp"); 

            yield return new WaitForSeconds(6.3f);

            _currentFloor = destination;
            destination = -1;

            OpenDoors_CurrentFloor();
            yield return new WaitForSeconds(1.5f);

            isMoving = false;
        }
        else if (_currentFloor > destination)
        {
            // Debug.Log("MoveToFloor() first");

            isMoving = true;
            CloseDoors_CurrentFloor();

            yield return new WaitForSeconds(1.5f);

            // play elevator moving sound
            elevatorBody_animator.Play("ElevatorMoveDown");

            yield return new WaitForSeconds(6.3f);

            _currentFloor = destination;
            destination = -1;

            OpenDoors_CurrentFloor();
            yield return new WaitForSeconds(1.5f);

            isMoving = false;
        }
        else // if the elevator got called from the same floor and the door is closed
        {
            isMoving = true;

            switch (_currentFloor)
            {
                case 0:
                    if (!door_0_isOpen)
                    {
                        OpenDoors_CurrentFloor();
                    }
                    break;
                case 1:
                    if (!door_1_isOpen)
                    {
                        OpenDoors_CurrentFloor();
                    }
                    break;
            }

            isMoving = false;
        }
    }

    /// <summary>
    /// Closes the doors on the current floor. (The method can be only called when isMoving is true.)
    /// </summary>
    private void CloseDoors_CurrentFloor()
    {
        if (isMoving)
        {
            switch (_currentFloor)
            {
                case 0:
                    door_0_animator.Play("Elevator_Door_Closing");
                    door_inside_animator.Play("Elevator_Door_Closing");
                    door_inside_isOpen = false;
                    door_0_isOpen = false;
                    // play sound
                    break;
                case 1:
                    door_1_animator.Play("Elevator_Door_Closing");
                    door_inside_animator.Play("Elevator_Door_Closing");
                    door_inside_isOpen = false;
                    door_1_isOpen = false;
                    // play sound
                    break;
                // mehet tovabb
            }
        }
    }

    /// <summary>
    /// Opens the doors on the current floor. (The method can be only called when isMoving is true.)
    /// </summary>
    private void OpenDoors_CurrentFloor()
    {
        if (isMoving)
        {
            switch (_currentFloor)
            {
                case 0:
                    door_0_animator.Play("Elevator_Door_Opening");
                    door_inside_animator.Play("Elevator_Door_Opening");
                    door_inside_isOpen = true;
                    door_0_isOpen = true;
                    // play sound
                    break;
                case 1:
                    door_1_animator.Play("Elevator_Door_Opening");
                    door_inside_animator.Play("Elevator_Door_Opening");
                    door_inside_isOpen = true;
                    door_1_isOpen = true;
                    // play sound
                    break;
                // mehet tovabb
            }
        }
    }
}
