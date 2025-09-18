using UnityEngine;

public class grow : MonoBehaviour
{
    [SerializeField] private Vector3 growe;
    [SerializeField] private float groweSpeed;
    

    // Update is called once per frame
    void Update()
    {
        transform.localScale += growe * groweSpeed * Time.deltaTime;
    }
}
