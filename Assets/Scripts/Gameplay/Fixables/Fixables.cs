using UnityEngine;
using FMODUnity;

public class Fixables : MonoBehaviour
{
    [SerializeField] public static bool isFixableAvalible = false;
    [SerializeField] private int fixableTaskCount = 0;

    private GameObject[] fixableList = {  };

    private void Awake()
    {
        fixableList = GameObject.FindGameObjectsWithTag("Fixable");
    }

    private void Start()
    {
        SubToEvents();
    }

    private float timeHeld = 0f;
    void Update()
    {
        if (Player.LookingAtTarget("Fixable", KeyCode.E) && timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX)
        {
            GameObject go = Player.LookingAt();
            LocalStorage fixableStorage = go.GetComponent<LocalStorage>();

            if (!fixableStorage.isFixed)
            {
                bool isFixed = fixableStorage.isFixed;
                Debug.Log($"timeheld = {Mathf.Round(timeHeld)} | isFixed = {isFixed}");

                if (Mathf.Round(timeHeld) >= ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                {
                    fixableStorage.FixThis();

                    
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

                        if (fixableTaskCount == 1)
                        {
                            fixableTaskCount--; 
                            isFixableAvalible = false;
                        }
                        else
                        {
                            fixableTaskCount--;
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
    }

    /// <summary>
    /// Damages a random Fixable at a random time if called from ContinousManager.cs
    /// </summary>
    public void DamageRandomFixable()
    {
        int randomEvent = Random.Range(45, 45); // should be defined in ValueStorage for different scenarios

        if (randomEvent == 45 && fixableTaskCount < fixableList.Length)
        {
            int randomIndex = Random.Range(0, fixableList.Length);
            LocalStorage fixableStorage = fixableList[randomIndex].GetComponent<LocalStorage>();

            if (fixableStorage.isFixed) 
            {
                fixableStorage.DamageThis();

                isFixableAvalible = true;
                fixableTaskCount++;

                Debug.Log($"Damaged a fixable. | fixableTaskCount = {fixableTaskCount}");
            }
        }
    }

    /// <summary>
    /// Decreases fixableTaskCount
    /// </summary>
    private void SubFixalbeTask()
    {
        if (fixableTaskCount == 1)
        {
            fixableTaskCount--;
            isFixableAvalible = false;
            return;
        }
        fixableTaskCount--;
    }

    // ----  Submethods  ---- //

    /// <summary>
    /// Subscribes the SubFixalbeTask() to all of the fixable object's fixTrigger event.
    /// </summary>
    private void SubToEvents()
    {
        foreach (GameObject fixableObject in fixableList)
        {
            fixableObject.GetComponent<LocalStorage>().FixTrigger += SubFixalbeTask;
        }
    }
}
