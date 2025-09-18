using UnityEngine;
using UnityEngine.UIElements;

public class DayNight : MonoBehaviour
{
    [SerializeField] private Vector3 dayRotation;
    [SerializeField] private Vector3 nightRotation;
    [SerializeField] private float daySpeed;
    [SerializeField] private float nightSpeed;

    [SerializeField] private Vector3 DayCycle;

    private float previousTime = 0;
    void Update()
    {
        if (transform.rotation.x < previousTime) // 
        {
            //Debug.Log($"NIGHT, {transform.rotation.x}");
            transform.Rotate(nightRotation * nightSpeed * Time.deltaTime);
            previousTime = transform.rotation.x;
        }
        else if (transform.rotation.x > previousTime) //
        {
            //Debug.Log($"DAY, {transform.rotation.x}");
            transform.Rotate(dayRotation * daySpeed * Time.deltaTime);
            previousTime = transform.rotation.x;
        }
        else
        {
            transform.Rotate(nightRotation * nightSpeed * Time.deltaTime);
            //Debug.Log($"DEMIÉRT, {transform.rotation.x}");
        }
    }
}
