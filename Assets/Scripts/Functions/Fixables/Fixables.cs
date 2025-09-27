using UnityEngine;

public class Fixables : MonoBehaviour
{
    [SerializeField] public static bool isFixableAvalible = false;
    [SerializeField] public static int fixableAvalibleCount = 0;

    private GameObject[] FixableList = {  };

    private void Awake()
    {
        FixableList = GameObject.FindGameObjectsWithTag("Fixable");
    }

    private float timeHeld = 0f;
    private float timeNeedToHeld = 5f;
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && timeHeld < timeNeedToHeld)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Fixable"))
                {
                    bool isFixed = hit.collider.GetComponent<VariableStorage_Fixable>().isFixed;
                    Debug.Log($"timeheld = {Mathf.Round(timeHeld)} | isFixed = {isFixed}");

                    if (Mathf.Round(timeHeld) >= timeNeedToHeld && !isFixed)
                    {
                        Debug.Log("fixed!");
                        isFixed = true;
                        hit.collider.GetComponent<ParticleSystem>().startLifetime = 1;
                        if (fixableAvalibleCount == 1)
                        {
                            fixableAvalibleCount--;
                            isFixableAvalible = false;
                        }
                        else
                        {
                            fixableAvalibleCount--;
                        }
                        timeHeld = 0f;
                    }
                    else if (timeHeld < timeNeedToHeld && !isFixed)
                    {
                        timeHeld += Time.deltaTime;
                    }
                }
            }
        }
        /*else
        {
            Debug.Log("elengedve");
            timeHeld = 0f;
        }*/

        if (!Input.GetKey(KeyCode.E) && fixableAvalibleCount > 0 && timeHeld != 0) // if the player releases the button [E] then 
        {
            Debug.Log("elengedve");
            timeHeld = 0f;
        }
    }

    /// <summary>
    /// Damages a random Fixable at a random time if called from ContinousManager.cs
    /// </summary>

    bool temp = true;  
    public void DamageRandomFixable()
    {
        int randomEvent = Random.Range(45, 45); // should be defined in ValueStorage for different scenarios

        if (randomEvent == 45 && temp)
        {
            temp = false;
            int randomIndex = Random.Range(0, FixableList.Length);
            FixableList[randomIndex].GetComponent<VariableStorage_Fixable>().isFixed = false;
            FixableList[randomIndex].GetComponent<ParticleSystem>().startLifetime = 1;

            isFixableAvalible = true;
            fixableAvalibleCount++;

            Debug.Log($"Damaged a fixable. | isFixableAvalible = {isFixableAvalible}");
        }
    }
}
