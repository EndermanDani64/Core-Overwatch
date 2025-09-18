using UnityEngine;
using TMPro;

public class Rotation : MonoBehaviour
{
    [SerializeField] private GameObject excludedObject;
    [SerializeField] private Transform canvasTransform;

    private Vector3 moveOffset = new Vector3(185, 0, 0);
    private Vector3 moveOffsetMain = new Vector3(85, 0, 0);
    private int lastPos = 0;


    void Update()
    {
        if (Input.mousePosition.x < (Screen.width / 3))
        {
            Debug.Log($"Egér a BAL oldalon van, {lastPos}"); //-22
            foreach (Transform child in canvasTransform)
            {
                if (child.gameObject != excludedObject && excludedObject && lastPos == 0)
                {
                    child.localPosition -= moveOffset;
                }
            }
            lastPos = -1;
        }
        else if (Input.mousePosition.x > (Screen.width / 3) * 2)
        {
            foreach (Transform child in canvasTransform)
            {
                if (child.gameObject != excludedObject && lastPos == 0)
                {
                    child.localPosition += moveOffset;
                }
                else if (child.gameObject == excludedObject && lastPos == 0)
                {
                    child.localPosition += moveOffsetMain;
                }
            }
            Debug.Log($"Egér a JOBB oldalon van, {lastPos}");
            lastPos = 1;
        }
        else
        {
            Debug.Log($"Egér a KÖZÉPEN oldalon van, {lastPos}");
            foreach (Transform child in canvasTransform)
            {
                if (child.gameObject != excludedObject && lastPos == -1)
                {
                    child.localPosition += moveOffset;
                }
                else if (child.gameObject != excludedObject && lastPos == 1)
                {
                    child.localPosition -= moveOffset;
                }
            }
            lastPos = 0;
        }
    }
}
