using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ElectricityDecreaseValueManagger : MonoBehaviour
{
    [SerializeField] OverallDoorManagger DoorManagger;
    [SerializeField] ElectricityManagger ElectricityManagger;
    [SerializeField] FanOverwatch FanOverwatch;
    [SerializeField] BlackoutEvent BlackoutEvent;

    //public int electricity_doorDecreaseValue = 0;
    [SerializeField] public List<int> decreaseList = new List<int> { 0, 0, 0, 0 }; // 0i: doors | 1i: controlrods | 2i: fans | 3i: lights
    [SerializeField] public static float decreaseValue = 0;

    public void DValueUpdate()
    {
        //Debug.Log($"DoorManagger.doorsStatus = {DoorManagger.doorsStatus}");
        if (DoorManagger.doorsStatus)
        {
            decreaseList[0] = ValueStorage.ELECTRICITY_MANUALDOORSCOST;
        }
        else
        {
            decreaseList[0] = 0;
        }

        if (FanOverwatch.fanAmountOnline > 0) // fans
        {
            decreaseList[2] = ValueStorage.ELECTRICITY_FANCOST * FanOverwatch.fanAmountOnline;
        }
        else
        {
            decreaseList[2] = 0;
        }

        if (!BlackoutEvent.isBlackout) // light
        {
            decreaseList[3] = ValueStorage.ELECTRICITY_LIGHTCOST;
        }
        else
        {
            decreaseList[3] = 0;
        }


        // itt adjuk össze a decreaseValue-ba az elhasznált energiát
        decreaseValue = 0;
        for (int i = 0; i < decreaseList.Count && decreaseValue < 26; i++)
        {
            decreaseValue += decreaseList[i];
        }
    }

    private IEnumerator DValueManagger()
    {
        for (int i = 0; i < decreaseList.Count; i++)
        {
            Debug.Log($"decreaseList[i] = {decreaseList[i]}");
            decreaseValue += decreaseList[i];
        }
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (ElectricityManagger.electricity <= 0)
        {
            //DoorManagger.DisableManualDoors();
        }
    }
}
