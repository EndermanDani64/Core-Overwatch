using UnityEngine;
using System.Collections;

public class Fixables : MonoBehaviour
{
    public int AvalibleTaskCount = ValueStorage.FIXABLES_MAXTASK;
    public bool IsTaskActive = false;
    public bool IsTaskAvalible = true;

    [SerializeField] private int _activeTaskCount = 0;
    private GameObject[] _fixableObjectList = {  };


    private void Awake()
    {
        _fixableObjectList = GameObject.FindGameObjectsWithTag("Fixable");
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
        if (AvalibleTaskCount <= 0 || !IsTaskAvalible) 
        { 
            Debug.LogWarning("No fixable is avalible!");
            return;
        }
        
        if (AvalibleTaskCount > 1)
        {
            AvalibleTaskCount--;
        }
        else if (AvalibleTaskCount == 1)
        {
            AvalibleTaskCount--;
            Cooldown(ValueStorage.FIXABLES_COOLDOWNBETWEENDAMAGING);
        }

        int randomEvent = Random.Range(45, 45); // should be defined in ValueStorage for different scenarios

        if (randomEvent == 45 && _activeTaskCount < _fixableObjectList.Length)
        {
            int randomIndex = Random.Range(0, _fixableObjectList.Length);
            Fixable_LocalStorage fixableStorage = _fixableObjectList[randomIndex].GetComponent<Fixable_LocalStorage>();

            if (fixableStorage.isFixed) 
            {
                fixableStorage.DamageThis();

                IsTaskActive = true;
                _activeTaskCount++;

                Debug.Log($"Damaged a fixable. | activeTaskCount = {_activeTaskCount}");
            }
        }
    }

    private IEnumerator Cooldown(float seconds)
    {
        IsTaskAvalible = false;
        yield return new WaitForSeconds(seconds);
        IsTaskAvalible = true;
    }

    /// <summary>
    /// Decreases fixableTaskCount
    /// </summary>
    private void SubFixalbeTask()
    {
        if (AvalibleTaskCount == ValueStorage.FIXABLES_MAXTASK)
        {
            Debug.LogWarning("Cannot decrease fixableTaskCount no more!");
            return;
        }
        if (AvalibleTaskCount >= 1 && AvalibleTaskCount > 0)
        {
            _activeTaskCount--;
            AvalibleTaskCount++;
            IsTaskActive = false;
            return;
        }
        _activeTaskCount--;
        AvalibleTaskCount++;
    }

    // ----  Submethods  ---- //

    /// <summary>
    /// Subscribes the SubFixalbeTask() to all of the fixable object's fixTrigger event.
    /// </summary>
    private void SubToEvents()
    {
        foreach (GameObject fixableObject in _fixableObjectList)
        {
            fixableObject.GetComponent<Fixable_LocalStorage>().FixTrigger += SubFixalbeTask;
        }
    }
}
