using UnityEngine;

public class Fixables : MonoBehaviour
{
    [SerializeField] public bool isFixableAvalible = false;

    private GameObject[] FixableList = {  };

    private void Awake()
    {
        FixableList = GameObject.FindGameObjectsWithTag("Fixable");
    }

    private float timeHeld = 0f;
    private float timeNeedToHeld = 5f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Fixable"))
                {
                    bool isFixed = hit.collider.GetComponent<VariableStorage_Fixable>().isFixed;

                    if (timeHeld >= timeNeedToHeld && isFixed)
                    {
                        isFixed = true;
                    }
                    else if (timeHeld < timeNeedToHeld && isFixed)
                    {
                        timeHeld += Time.deltaTime;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Damages a random Fixable at a random time if called from ContinousManager.cs
    /// </summary>
    public void DamageRandomFixable()
    {
        int randomEvent = Random.Range(0, 500); // should be defined in ValueStorage for different scenarios

        if (randomEvent == 45)
        {
            int randomIndex = Random.Range(0, FixableList.Length);
            FixableList[randomIndex].GetComponent<VariableStorage_Fixable>().isFixed = false;
            Debug.Log("Damaged a fixable.");
        }
    }
}
