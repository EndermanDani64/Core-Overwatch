using UnityEngine;
using UnityEngine.UI;

public class ControlRodManager : MonoBehaviour
{
    [SerializeField] private Slider controlRod1;
    [SerializeField] private Slider controlRod2;
    [SerializeField] private Slider controlRod3;
    [SerializeField] private Slider controlRod4;

    public void SetControlRodValue(float value)
    {
        controlRod1.value = value;
        controlRod2.value = value;
        controlRod3.value = value;
        controlRod4.value = value;
    }
}
