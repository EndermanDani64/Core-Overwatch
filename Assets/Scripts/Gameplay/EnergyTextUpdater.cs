using TMPro;
using UnityEngine;

public class EnergyTextUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void textUpdate()
    {
        text.text = $"Energy: {Mathf.Round(ElectricityManagger.electricity)}";
    }
}
