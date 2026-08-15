using System.Collections;
using UnityEngine;

public class ECoolantController : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button Rod1;
    [SerializeField] UnityEngine.UI.Button Rod2;
    [SerializeField] UnityEngine.UI.Button Rod3;
    [SerializeField] UnityEngine.UI.Button Rod4;
    [SerializeField] UnityEngine.UI.Button Rod5;

    [SerializeField] Meltdown MeltdownEvent;
    [SerializeField] private Timer timer;

    [SerializeField] private SupplyDeposit SupplyDeposit;
    [SerializeField] private ShoutSystem shoutSystem;

    public int RodSuccess = 0;
    public bool isECoolantEventActive = false;
    public bool isECoolantSucces = false;

    public void StartIEnumerator()
    {
        Debug.Log("StartIEnumator");
        StartCoroutine(ECoolantEventManagger());
    }

    IEnumerator ECoolantEventManagger()
    {
        if (SupplyDeposit.supplyedValue == 0 || SupplyDeposit.supplyedValue < 0)
        {
            shoutSystem.ShowMessage("WARNING, ECoolant CANNOT BE USED BECAUSE OF THE LACK OF SUPPLY!");
            RodSuccess = 0;
            CheckECoolantSuccess();
            Debug.LogWarning("RodSuccess was 0.");
        }
        else
        {
            Rod1.interactable = true;
            Rod2.interactable = true;
            Rod3.interactable = true;
            Rod4.interactable = true;
            Rod5.interactable = true;
            isECoolantEventActive = true;
            timer.StartTimer();
            yield return new WaitForSeconds(93.5f);

            Rod1.interactable = false;
            Rod2.interactable = false;
            Rod3.interactable = false;
            Rod4.interactable = false;
            Rod5.interactable = false;
            isECoolantEventActive = false;
            yield return new WaitForSeconds(.5f);
            Debug.LogWarning($"RodSuccess is {RodSuccess}");
            Debug.Log("ECOOLANT EVENT OFF");
            CheckECoolantSuccess();
        }
    }

    public void CheckECoolantSuccess()
    {
        int randomSuccess = Random.Range(0, 100);
        if (RodSuccess == 5) // | 5 - 95% | 4 - 80% | 3 - 65% | 2 - 35% | 1 - 10% |
        {
            //Debug.Log("Chanse: 95%");
            if (randomSuccess < 95)
            {
                isECoolantSucces = true;
            }
        }
        else if (RodSuccess == 4)
        {
            //Debug.Log("Chanse: 80%");
            if (randomSuccess < 80)
            {
                isECoolantSucces = true;
            }
        }
        else if (RodSuccess == 3)
        {
            //Debug.Log("Chanse: 70%");
            if (randomSuccess < 70)
            {
                isECoolantSucces = true;
            }
        }
        else if (RodSuccess == 2)
        {
            //Debug.Log("Chanse: 45%");
            if (randomSuccess < 45)
            {
                isECoolantSucces = true;
            }
        }
        else if (RodSuccess == 1)
        {
            //Debug.Log("Chanse: 35%");
            if (randomSuccess < 35)
            {
                isECoolantSucces = true;
            }
        }
    }

}
