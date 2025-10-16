using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    private Collider damageCollider;
    private Health playerHealth;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
    }

    private float delay = 5f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && delay >= 5)
        {
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }
            
            playerHealth.health -= ValueStorage.HEALTH_LIQUID_DAMAGE;
            delay = 0f;
        }
        else if (collision.gameObject.CompareTag("Player") && delay < 5)
        {
            delay += Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            delay = 0f;
            playerHealth = null;
        }
    }
}
