using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float waitTime = 10f;  // Mennyi idõ után indul a mozgás
    [SerializeField] private float moveSpeed = 1f;  // Felfelé mozgás sebessége

    private bool startMoving = false;

    private void Start()
    {
        StartCoroutine(StartMovingAfterDelay());
    }

    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(waitTime); // 10 mp várakozás
        startMoving = true; // Indítás engedélyezése
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime; // Kocka mozgatása
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ha a Player rááll a kockára
        {
            other.transform.SetParent(transform); // A Player a kocka gyermekévé válik
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ha a Player elhagyja a kockát
        {
            other.transform.SetParent(null); // A Player leválik a kockáról
        }
    }
}
