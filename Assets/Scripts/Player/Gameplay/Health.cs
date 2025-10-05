using UnityEngine;

public class Health : MonoBehaviour
{
    public static bool god = false;
    [SerializeField] public /*static*/ float health = ValueStorage.PLAYER_HEALTH_MAX;

    [SerializeField] public Transform groundCheck;

    [SerializeField] private float timeFallen = 0f;
    private bool isGrounded = false;

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.red);

        if (!isGrounded)
        {
            timeFallen += Time.deltaTime;
        }
        else
        {
            if (timeFallen > 1.2f)
            {
                health -= Mathf.Round(1 * timeFallen);
            }
            timeFallen = 0f;
        }
    }
}
