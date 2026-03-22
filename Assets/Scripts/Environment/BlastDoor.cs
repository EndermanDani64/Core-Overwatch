using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BlastDoor : MonoBehaviour
{
    [SerializeField] private Transform Door;
    [SerializeField] private Transform PointA; // Closed position
    [SerializeField] private Transform PointB; // Open position

    public float speed = .3f;
    private bool isOpen = true;
    private bool isMoving = false;

    public void ToggleDoor()
    {
        //Debug.Log($"isMoving = {isMoving}");
        EventSystem.current.SetSelectedGameObject(null);
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(isOpen ? PointA.position : PointB.position));
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        isMoving = true;

        if (isOpen)
        {
            //Debug.Log("Closing");
            while (Vector3.Distance(Door.position, PointA.position) > 0.05f)
            {
                Door.position = Vector3.MoveTowards(Door.position, PointA.position, speed * Time.deltaTime);
                if (Door.position == PointA.position)
                {
                    break;
                }
                //Debug.Log($"opening, Distance = {Vector3.Distance(Door.position, PointB.position)}, isOpen = {isOpen}");
                yield return null;
            }
        }
        else
        {
            //Debug.Log("Opening");
            while (Vector3.Distance(Door.position, PointB.position) > 0.05f)
            {
                Door.position = Vector3.MoveTowards(Door.position, PointB.position, speed * Time.deltaTime);
                if (Door.position == PointB.position)
                {
                    break;
                }
                //Debug.Log($"closing, Distance = {Vector3.Distance(Door.position, PointB.position)}, isOpen = {isOpen}");
                yield return null;
            }
        }

        //Debug.Log("Exited");

        //Door.position = targetPosition;
        isOpen = !isOpen;
        isMoving = false;

        //Debug.Log($"Door.position = {Door.position}");
        //Debug.Log($"targetPosition = {targetPosition}");
        //Debug.Log($"isOpen = {isOpen}");

    }


}
