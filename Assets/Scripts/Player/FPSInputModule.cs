using UnityEngine;
using UnityEngine.EventSystems;

public class FPSInputModule : StandaloneInputModule
{
    protected override MouseState GetMousePointerEventData(int id = 0)
    {
        var reticlePosition = new Vector2(Screen.width / 2, Screen.height / 2);

        MouseState mouseState = new MouseState();

        PointerEventData leftData;
        var created = GetPointerData(kMouseLeftId, out leftData, true);

        leftData.Reset();

        leftData.delta = reticlePosition - leftData.position;
        leftData.position = reticlePosition;
        leftData.scrollDelta = Input.mouseScrollDelta;
        leftData.button = PointerEventData.InputButton.Left;
        eventSystem.RaycastAll(leftData, m_RaycastResultCache);
        var raycast = FindFirstRaycast(m_RaycastResultCache);
        leftData.pointerCurrentRaycast = raycast;
        m_RaycastResultCache.Clear();

        // copy the apropriate data into right and middle slots
        PointerEventData rightData;
        GetPointerData(kMouseRightId, out rightData, true);
        CopyFromTo(leftData, rightData);
        rightData.button = PointerEventData.InputButton.Right;

        PointerEventData middleData;
        GetPointerData(kMouseMiddleId, out middleData, true);
        CopyFromTo(leftData, middleData);
        middleData.button = PointerEventData.InputButton.Middle;

        mouseState.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0), leftData);
        mouseState.SetButtonState(PointerEventData.InputButton.Right, StateForMouseButton(1), rightData);
        mouseState.SetButtonState(PointerEventData.InputButton.Middle, StateForMouseButton(2), middleData);

        return mouseState;
    }
}