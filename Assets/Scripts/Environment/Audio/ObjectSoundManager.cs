using UnityEngine;

public class ObjectSoundManager : MonoBehaviour
{
    [SerializeField] private FMODUnity.StudioEventEmitter[] eventEmmiters;
    
    public void PlaySound(int index)
    {
        if (index < 0 || index >= eventEmmiters.Length) return;

        eventEmmiters[index].Play();
    }
}
