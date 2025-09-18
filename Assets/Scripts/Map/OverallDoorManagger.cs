using UnityEngine;
using UnityEngine.UI;

public class OverallDoorManagger : MonoBehaviour
{
    //[SerializeField] private ValueStorage ValueStorage; most jottem ra h nem kell valtozoban tarolni ezeket (legalabb is nem kell minden kis cuccokhoz)
    [SerializeField] ElectricityDecreaseValueManagger DecreaseEnergy;

    [SerializeField] GameObject[] taggedObjects = { };
    public bool doorsStatus = true;

    private void Start()
    {
        taggedObjects = GameObject.FindGameObjectsWithTag("ManualDoor");
    }
    
    public void DisableManualDoors()
    {
        if (doorsStatus)
        {
            foreach (GameObject obj in taggedObjects)
            {
                obj.GetComponent<Button>().interactable = false;
                Debug.Log($"button.interactable = {obj.GetComponent<Button>().interactable}");
            }
            doorsStatus = !doorsStatus;
        }
    }
    public void EnableManualDoors()
    {
        if (!doorsStatus)
        {
            foreach (GameObject obj in taggedObjects)
            {
                Button button = obj.GetComponent<Button>();
                button.interactable = true;
                Debug.Log($"button.interactable = {button.interactable}");
            }
            doorsStatus = !doorsStatus;
        }
    }

    /*void Update()
    {
        DecreaseEnergy.electricity_doorDecreaseValue = electricity_doorsCost;
    }*/
}
