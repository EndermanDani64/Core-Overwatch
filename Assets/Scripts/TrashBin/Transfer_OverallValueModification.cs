using UnityEngine;

public class Transfer_OverallValueModification : MonoBehaviour
{
    [SerializeField] private TempController tempController;
    private void Start()
    {
        if (PlayManagger.START_ISONLINE)
        {
            tempController.Transfer_StartReactor(1);
        }
    }
}
