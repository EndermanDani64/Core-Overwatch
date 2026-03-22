using UnityEngine;

public class Ambience : MonoBehaviour
{
    public GameObject player;
    public Collider area;

    void Update()
    {
        Vector3 closestPoint = area.ClosestPoint(player.transform.position);
        transform.position = closestPoint;
    }
}
