using System.Collections;
using UnityEngine;

public class TESTY : MonoBehaviour
{
    public int RodSuccess = 0;
    void Start()
    {
        StartCoroutine(aaaa());
    }

    private IEnumerator aaaa()
    {
        while (true)
        {
            if (RodSuccess == 5) // | 5 - 95% | 4 - 80% | 3 - 65% | 2 - 35% | 1 - 10% |
            {
                Debug.Log("Chanse: 95%");
                int randomSuccess = Random.Range(0, 100);
                if (randomSuccess < 95)
                {
                    Debug.Log("NIGGA - SUCCESS");
                }
                else
                {
                    Debug.Log("NIGGA - FAIL");
                }
            }
            else if (RodSuccess == 4)
            {
                Debug.Log("Chanse: 80%");
                int randomSuccess = Random.Range(0, 100);
                if (randomSuccess < 80)
                {
                    Debug.Log("NIGGA - SUCCESS");
                }
                else
                {
                    Debug.Log("NIGGA - FAIL");
                }
            }
            else if (RodSuccess == 3)
            {
                Debug.Log("Chanse: 65%");
                int randomSuccess = Random.Range(0, 100);
                if (randomSuccess < 65)
                {
                    Debug.Log("NIGGA - SUCCESS");
                }
                else
                {
                    Debug.Log("NIGGA - FAIL");
                }
            }
            else if (RodSuccess == 2)
            {
                Debug.Log("Chanse: 35%");
                int randomSuccess = Random.Range(0, 100);
                if (randomSuccess < 35)
                {
                    Debug.Log("NIGGA - SUCCESS");
                }
                else
                {
                    Debug.Log("NIGGA - FAIL");
                }
            }
            else if (RodSuccess == 1)
            {
                Debug.Log("Chanse: 10%");
                int randomSuccess = Random.Range(0, 100);
                if (randomSuccess < 15)
                {
                    Debug.Log("NIGGA - SUCCESS");
                }
                else
                {
                    Debug.Log("NIGGA - FAIL");
                }
            }
            yield return new WaitForSeconds(2.5f);
        }
    }
}
