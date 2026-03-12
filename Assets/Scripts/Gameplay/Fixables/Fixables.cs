using UnityEngine;

public class Fixables : MonoBehaviour
{
    [SerializeField] public static bool isFixableAvalible = false;
    [SerializeField] private int fixableAvalibleCount = 0;

    private GameObject[] fixableList = {  };

    private void Awake()
    {
        fixableList = GameObject.FindGameObjectsWithTag("Fixable");
    }

    private float timeHeld = 0f;
    void Update()
    {
        if (Player.LookingAtTarget("Fixable", KeyCode.E) && timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX)
        {
            GameObject go = Player.LookingAt();

            if (!go.GetComponent<VariableStorage_Fixable>().isFixed)
            {
                bool isFixed = go.GetComponent<VariableStorage_Fixable>().isFixed;
                Debug.Log($"timeheld = {Mathf.Round(timeHeld)} | isFixed = {isFixed}");

                if (Mathf.Round(timeHeld) >= ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                {
                    go.GetComponent<VariableStorage_Fixable>().isFixed = true;
                    go.GetComponent<ParticleSystem>().startLifetime = 0;
                    go.GetComponent<AudioSource>().PlayOneShot(go.GetComponent<VariableStorage_Fixable>().fixingSFX);

                    if (fixableAvalibleCount == 1)
                    {
                        fixableAvalibleCount--;
                        isFixableAvalible = false;
                    }
                    else
                    {
                        fixableAvalibleCount--;
                    }
                    Debug.Log("fixed!");
                    timeHeld = 0f;
                }
                else if (timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                {
                    timeHeld += Time.deltaTime;
                }
            }
        }

        /*if (Input.GetKey(KeyCode.E) && timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Fixable") && !hit.collider.GetComponent<VariableStorage_Fixable>().isFixed)
                {
                    bool isFixed = hit.collider.GetComponent<VariableStorage_Fixable>().isFixed;
                    Debug.Log($"timeheld = {Mathf.Round(timeHeld)} | isFixed = {isFixed}");

                    if (Mathf.Round(timeHeld) >= ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                    {
                        hit.collider.GetComponent<VariableStorage_Fixable>().isFixed = true;
                        hit.collider.GetComponent<ParticleSystem>().startLifetime = 0;
                        hit.collider.GetComponent<AudioSource>().PlayOneShot(hit.collider.GetComponent<VariableStorage_Fixable>().fixingSFX);

                        if (fixableAvalibleCount == 1)
                        {
                            fixableAvalibleCount--; 
                            isFixableAvalible = false;
                        }
                        else
                        {
                            fixableAvalibleCount--;
                        }
                        Debug.Log("fixed!");
                        timeHeld = 0f;
                    }
                    else if (timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                    {
                        timeHeld += Time.deltaTime;
                    }
                }
            }
        }*/

        Upd_CheckHolding();
    }

    /// <summary>
    /// Damages a random Fixable at a random time if called from ContinousManager.cs
    /// </summary>
    public void DamageRandomFixable()
    {
        int randomEvent = Random.Range(45, 45); // should be defined in ValueStorage for different scenarios

        if (randomEvent == 45 && fixableAvalibleCount < fixableList.Length)
        {
            int randomIndex = Random.Range(0, fixableList.Length);
            if (fixableList[randomIndex].GetComponent<VariableStorage_Fixable>().isFixed) 
            {
                fixableList[randomIndex].GetComponent<VariableStorage_Fixable>().isFixed = false;
                fixableList[randomIndex].GetComponent<ParticleSystem>().startLifetime = 0.13f;

                isFixableAvalible = true;
                fixableAvalibleCount++;

                Debug.Log($"Damaged a fixable. | fixableAvalibleCount = {fixableAvalibleCount}");
            }
        }
    }

    // ----  Submethods  ---- //

    private void Upd_CheckHolding()
    {
        if (!Input.GetKey(KeyCode.E) && fixableAvalibleCount > 0 && timeHeld != 0) // if the player releases the button [E] then 
        {
            Debug.Log("elengedve");
            timeHeld = 0f;
        }
    }
}
