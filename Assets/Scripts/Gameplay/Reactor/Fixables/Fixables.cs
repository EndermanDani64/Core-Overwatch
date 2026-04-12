using UnityEngine;
using FMODUnity;
using System.Collections;

public class Fixables : MonoBehaviour
{
    [SerializeField] public static bool isTaskActive = false;
    [SerializeField] private int activeTaskCount = 0;

    public int avalibleTaskCount = ValueStorage.FIXABLES_MAXTASK;
    public bool isTaskAvalible = true;

    private GameObject[] fixableList = {  };

    private void Awake()
    {
        fixableList = GameObject.FindGameObjectsWithTag("Fixable");
    }

    private void Start()
    {
        SubToEvents();
    }

    /*private float timeHeld = 0f;
    void Update()
    {
        if (Player.LookingAtTag("Fixable", KeyCode.E) && timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX)
        {
            GameObject go = Player.GetLookedAtObject();
            Fixable_LocalStorage fixableStorage = go.GetComponent<Fixable_LocalStorage>();

            if (!fixableStorage.isFixed)
            {
                bool isFixed = fixableStorage.isFixed;
                Debug.Log($"timeheld = {Mathf.Round(timeHeld)} | isFixed = {isFixed}");

                if (Mathf.Round(timeHeld) >= ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                {
                    Debug.Log("FIXED");
                    fixableStorage.FixThis();

                    
                    timeHeld = 0f;
                }
                else if (timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX && !isFixed)
                {
                    timeHeld += Time.deltaTime;
                }
            }
        }

        if (Input.GetKey(KeyCode.E) && timeHeld < ValueStorage.TIME_FIXABLE_TIMETOFIX)
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
        }
    }*/

    /// <summary>
    /// Damages a random Fixable at a random time if called from ContinousManager.cs
    /// </summary>
    public void DamageRandomFixable()
    {
        if (avalibleTaskCount <= 0 || !isTaskAvalible) 
        { 
            Debug.LogWarning("No fixable is avalible!");
            return;
        }
        
        if (avalibleTaskCount > 1)
        {
            avalibleTaskCount--;
        }
        else if (avalibleTaskCount == 1)
        {
            avalibleTaskCount--;
            Cooldown(ValueStorage.FIXABLES_COOLDOWNBETWEENDAMAGING);
        }

        int randomEvent = Random.Range(45, 45); // should be defined in ValueStorage for different scenarios

        if (randomEvent == 45 && activeTaskCount < fixableList.Length)
        {
            int randomIndex = Random.Range(0, fixableList.Length);
            Fixable_LocalStorage fixableStorage = fixableList[randomIndex].GetComponent<Fixable_LocalStorage>();

            if (fixableStorage.isFixed) 
            {
                fixableStorage.DamageThis();

                isTaskActive = true;
                activeTaskCount++;

                Debug.Log($"Damaged a fixable. | activeTaskCount = {activeTaskCount}");
            }
        }
    }

    private IEnumerator Cooldown(float seconds)
    {
        isTaskAvalible = false;
        yield return new WaitForSeconds(seconds);
        isTaskAvalible = true;
    }

    /// <summary>
    /// Decreases fixableTaskCount
    /// </summary>
    private void SubFixalbeTask()
    {
        if (avalibleTaskCount == ValueStorage.FIXABLES_MAXTASK)
        {
            Debug.LogWarning("Cannot decrease fixableTaskCount no more!");
            return;
        }
        if (avalibleTaskCount >= 1 && avalibleTaskCount > 0)
        {
            activeTaskCount--;
            avalibleTaskCount++;
            isTaskActive = false;
            return;
        }
        activeTaskCount--;
        avalibleTaskCount++;
    }

    // ----  Submethods  ---- //

    /// <summary>
    /// Subscribes the SubFixalbeTask() to all of the fixable object's fixTrigger event.
    /// </summary>
    private void SubToEvents()
    {
        foreach (GameObject fixableObject in fixableList)
        {
            fixableObject.GetComponent<Fixable_LocalStorage>().FixTrigger += SubFixalbeTask;
        }
    }
}
