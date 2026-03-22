using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    /// <summary>
    /// Calls the ElevatorBody to the floor's valued floor.
    /// </summary>
    public void GoToFloor()
    {
        if (!_eM.isMoving)
        {
            StartCoroutine(_eM.MoveToFloor(_designatedFloor));
        }
    }

    /// <summary>
    /// Calls the ElevatorBody to the floor's valued floor.
    /// </summary>
    public void CallToFloor(int targetFloor)
    {
        if (!_eM.isMoving)
        {
            StartCoroutine(_eM.MoveToFloor(targetFloor));
        }
    }

    [SerializeField] public int localFloor;
    [SerializeField] private int _designatedFloor;

    [SerializeField] private ElevatorManager _eM;
}
