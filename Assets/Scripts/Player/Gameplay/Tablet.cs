using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private bool _cooldown = false;
    public IEnumerator ToggleTablet()
    {
        if (_tabletEquiped && !_cooldown) // close
        {
            _tabletEquiped = false;
            _cooldown = true;
            _overlayUIManager.HideCursor();
            Player.IDisableDefaultInput();
            _playerMovment.ReleaseMouseY();

            _anim.Play("TabletClose");

            ToggleUI();

            yield return new WaitForSeconds(.3f);

            _cooldown = false;
        }
        else if (!_tabletEquiped && !_cooldown) // open
        {
            _tabletEquiped = true;
            _cooldown = true;
            _playerMovment.FixMouseY();

            _anim.Play("TabletOpen");

            ToggleUI();

            yield return new WaitForSeconds(.5f);

            Player.IEnableDefaultInput();
            _overlayUIManager.ShowCursor();

            yield return new WaitForSeconds(.3f);

            _cooldown = false;
        }
    }

    // ----  Submethods  ---- //

    private void ToggleUI()
    {
        _crosshair1.enabled = !_crosshair1.enabled;
        _crosshair2.enabled = !_crosshair2.enabled;
        _targetLabel.enabled = !_targetLabel.enabled;
    }

    // ----  Initialization  ---- //

    private bool _tabletEquiped = false;

    [SerializeField] private Movment _playerMovment;

    [SerializeField] private OverlayUIManager _overlayUIManager;

    // player UI stuff
    [SerializeField] private Image _crosshair1;
    [SerializeField] private Image _crosshair2;
    [SerializeField] private TMP_Text _targetLabel;

    private Animator _anim;
}
