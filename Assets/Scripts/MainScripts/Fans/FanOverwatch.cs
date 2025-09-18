using System.Collections.Generic;
using UnityEngine;

public class FanOverwatch : MonoBehaviour
{
    [Header("Fans for controlling them")]
    [SerializeField] private FanControl Fan1;
    [SerializeField] private FanControl Fan2;
    [SerializeField] private FanControl Fan3;
    [SerializeField] private FanControl Fan4;
    [SerializeField] private FanControl Fan5;

    [Header("A list for all the Fans")]
    [SerializeField] private List<FanControl> FanControllers;

    [Header("Important variables")]
    public static int fanAmountOnline = 0;
    
    public void ShutDownFan(int fanID)
    {
        switch (fanID)
        {
            case 1:
                Fan1.Transfer_ForceReset(); 
                break;
            case 2:
                Fan2.Transfer_ForceReset();
                break;
            case 3:
                Fan3.Transfer_ForceReset();
                break;
            case 4:
                Fan4.Transfer_ForceReset();
                break;
            case 5:
                Fan5.Transfer_ForceReset();
                break;
            default:
                Debug.Log($"Fan ID {fanID}. This is not an existing Fan.");
                break;
        }
    }

    public void Transfer_ForceResetAll()
    {
        foreach (var fan in FanControllers)
        {
            if (fan == null)
            {
                Debug.Log("The fan variable is not set in the Transfer_ForceResetAll function.");
            }
            else
            {
                fan.Transfer_ForceReset();
            }
        }
    }
}
