using System.Collections.Generic;
using UnityEngine;

public class FanOverwatch : MonoBehaviour
{

    // note: connect with ElectricityManager and increase the energy consumption there!

    public EnergyManagger electricityManaggerReference;

    [SerializeField] private FanControl Fan1;
    [SerializeField] private FanControl Fan2;
    [SerializeField] private FanControl Fan3;
    [SerializeField] private FanControl Fan4;
    [SerializeField] private FanControl Fan5;

    [SerializeField] private List<FanControl> FanControllers;

    public static int OnlineFanCount = 0;
    


    //public void 

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
