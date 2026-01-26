using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    private Health playerHealth;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && !HazmatSuit.isHazmat)
        {
            if (playerHealth == null)
            {
                playerHealth = collider.gameObject.GetComponent<Health>();
            }

            playerHealth.DamagePlayer(ValueStorage.HEALTH_LIQUID_DAMAGE, 0.8f, true, Time.deltaTime);
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            delay = 0f;
            playerHealth = null;
        }
    }*/
}
