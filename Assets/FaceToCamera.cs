using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    //[SerializeField] Camera camera;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);
    void Update()
    {
        /*transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);*/
        /*Transform camPos = camera.main.transform;
        transform.LookAt(camPos.position - camPos.forward);*/
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            // Kövesse a pozíciót, de nem a forgást
            transform.position = target.position + offset;

            // A rotáció mindig a világé legyen (vagy a kameráé, ha szeretnéd hogy "billboard" legyen)
            transform.LookAt(Camera.main.transform);
        }
    }
}
