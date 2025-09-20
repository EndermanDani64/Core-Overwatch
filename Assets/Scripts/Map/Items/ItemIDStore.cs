using UnityEngine;

public class ItemIDStore : MonoBehaviour
{
    [SerializeField] private ValueStorage ValueStorage;
    public int ID = 0;

    private void Start()
    {
        int randomID = Random.Range(0, 25);
        while (!ValueStorage.IsIDFree(randomID))
        {
            randomID = Random.Range(0, 25);
            Debug.Log($"ID was not reserved. {randomID} ID was tried.");
        }
        ValueStorage.ReserveID(randomID);
        ID = randomID;
        //Debug.Log($"ID reserved. ID: {randomID}");
    }

    private void IDApplication()
    {
        int randomID = Random.Range(0, 25);
        while (!ValueStorage.IsIDFree(randomID))
        {
            randomID = Random.Range(0, 25);
        }
        ValueStorage.ReserveID(randomID);
    }
}
