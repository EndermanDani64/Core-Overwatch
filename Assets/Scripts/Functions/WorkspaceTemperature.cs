using System.Collections.Generic;
using UnityEngine;

public class WorkspaceTemperature : MonoBehaviour
{
    [SerializeField] public float areaTemperature = 25f;

    private List<WorkspaceTemperature> _rooms = new List<WorkspaceTemperature>();

    private void FillList()
    {
        GameObject[] connectedRooms = GameObject.FindGameObjectsWithTag("RoomTemperature");

        foreach (GameObject room in connectedRooms)
        {
            _rooms.Add(room.GetComponent<WorkspaceTemperature>());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    void Update()
    {
        
    }
}
