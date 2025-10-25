using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorOperator : MonoBehaviour
{
    /*[SerializeField] GameObject button1;
    [SerializeField] GameObject button2;

    [SerializeField] GameObject elevatorPlatform;

    private bool isMoving = false;
    private int currentFloor = 0;
    private int floorDestination = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ElevatorButton"))
                {
                    floorDestination = hit.collider.gameObject.GetComponent<ButtonIdentifier>().floorNumber;
                    isMoving = true;
                }
            }
        }
    }

    IEnumerator MoveElevator()
    {
        while (elevatorPlatform.GetComponent<Transform>().position)
    }

    private void MoveElevator()
    {
        if (currentFloor != floorDestination)
        {
            elevatorPlatform.GetComponent<Transform>().position
        }
    }*/
}
