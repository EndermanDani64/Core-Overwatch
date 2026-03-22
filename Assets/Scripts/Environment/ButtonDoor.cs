using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private Button Button1;
    [SerializeField] private Button Button2;
    [SerializeField] private Transform Door;
    [SerializeField] private Transform PointA; // Closed position
    [SerializeField] private Transform PointB; // Open position

    [Space]

    [SerializeField] private ElectricityManagger electricityManagger;

    public float speed = 2f;
    private bool isOpen = false;
    private bool isMoving = false;

    public void ToggleDoor()
    {
        Debug.Log($"isMoving = {isMoving}");
        EventSystem.current.SetSelectedGameObject(null);
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(isOpen ? PointA.position : PointB.position));
        }
    }

    public void ToggleTimeDoor()
    {
        StartCoroutine(TimeDoorEnumerator());
    }

    private IEnumerator TimeDoorEnumerator()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (!isMoving)
        {
            StartCoroutine(MoveDoor(isOpen ? PointA.position : PointB.position));
            yield return new WaitForSeconds(8f);
            StartCoroutine(MoveDoor(isOpen ? PointA.position : PointB.position));
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        isMoving = true;
        //electricityManagger.usage += 

        if (isOpen)
        {
            while (Vector3.Distance(Door.position, PointA.position) > 0.05f)
            {
                Door.position = Vector3.MoveTowards(Door.position, PointA.position, speed * Time.deltaTime);
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
                Door.position = Vector3.MoveTowards(Door.position, PointB.position, speed * Time.deltaTime);
                if (Door.position == PointB.position)
                {
                    break;
                }
                //Debug.Log($"closing, Distance = {Vector3.Distance(Door.position, PointB.position)}, isOpen = {isOpen}");
                yield return null;
            }
        }

        isOpen = !isOpen;
        isMoving = false;

        //Debug.Log($"Door.position = {Door.position}");
        //Debug.Log($"targetPosition = {targetPosition}");
        //Debug.Log($"isOpen = {isOpen}");
    }


}
