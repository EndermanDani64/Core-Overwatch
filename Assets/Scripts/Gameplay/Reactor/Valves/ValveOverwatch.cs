using System.Collections.Generic;
using UnityEngine;

public class ValveOverwatch : MonoBehaviour
{
    [Header("Fans for controlling them")]
    [SerializeField] private ValveControl Valve1;
    [SerializeField] private ValveControl Valve2;
    [SerializeField] private ValveControl Valve3;
    [SerializeField] private ValveControl Valve4;
    [SerializeField] private ValveControl Valve5;

    [Header("A list for all the Fans")]
    [SerializeField] private List<FanControl> ValveControllers;

    [Header("Important variables")]
    public static int valveAmountOpen = 0;

    public void CloseValve(int valveID)
    {
        switch (valveID)
        {
            case 1:
                Valve1.Transfer_ForceReset();
                break;
            case 2:
                Valve2.Transfer_ForceReset();
                break;
            case 3:
                Valve3.Transfer_ForceReset();
                break;
            case 4:
                Valve4.Transfer_ForceReset();
                break;
            case 5:
                Valve5.Transfer_ForceReset();
                break;
            default:
                Debug.Log($"Valve ID {valveID}. This is not an existing Fan.");
                break;
        }
    }

    public void Transfer_ForceResetAll()
    {
        foreach (var valve in ValveControllers)
        {
            if (valve == null)
            {
                Debug.Log("The valve variable is not set in the Transfer_ForceResetAll function.");
            }
            else
            {
                valve.Transfer_ForceReset();
            }
        }
    }
}
