using UnityEngine;

public class DEV_PanelManager : MonoBehaviour
{
    [SerializeField] private Canvas panel1;
    [SerializeField] private Canvas panel2;
    //public bool anyPanelOpen = false;

    public void DEV_TogglePanel1()
    {
        if (!panel1.enabled)
        {
            panel2.enabled = false;
            panel1.enabled = true;
        }
        else
        {
            panel1.enabled = false;
        }
    }
    public void DEV_TogglePanel2()
    {
        if (!panel2.enabled)
        {
            panel1.enabled = false;
            panel2.enabled = true;
        }
        else
        {
            panel2.enabled = false;
        }
    }
}
