using UnityEngine;
using System.Collections;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private Transform Door;
    [SerializeField] private Transform PointA; // Closed position
    [SerializeField] private Transform PointB; // Open position

    [SerializeField] private bool isTimedDoor = false;
    
    private float _speed = 2f;
    private bool _isOpen = false;

    private Coroutine movingDoorCoroutine;

    public void ToggleDoor()
    {
        if (movingDoorCoroutine == null)
        {
            if (!isTimedDoor)
                movingDoorCoroutine = StartCoroutine(MoveDoor(_isOpen ? PointA.position : PointB.position));
            else
                movingDoorCoroutine = StartCoroutine(ToggleTimeDoor());
        }   
    }

    private IEnumerator ToggleTimeDoor()
    {
        StartCoroutine(MoveDoor(_isOpen ? PointA.position : PointB.position));
        yield return new WaitForSeconds(8f);
        StartCoroutine(MoveDoor(_isOpen ? PointA.position : PointB.position));

        movingDoorCoroutine = null;
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        if (_isOpen)
        {
            while (Vector3.Distance(Door.position, PointA.position) > 0.05f)
            {
                Door.position = Vector3.MoveTowards(Door.position, PointA.position, _speed * Time.deltaTime);
                if (Door.position == PointA.position)
                {
                    break;
                }
                yield return null;
            }
        }
        else
        {
            while (Vector3.Distance(Door.position, PointB.position) > 0.05f)
            {
                Door.position = Vector3.MoveTowards(Door.position, PointB.position, _speed * Time.deltaTime);
                if (Door.position == PointB.position)
                {
                    break;
                }
                yield return null;
            }
        }

        _isOpen = !_isOpen;
        movingDoorCoroutine = null;
    }
}
