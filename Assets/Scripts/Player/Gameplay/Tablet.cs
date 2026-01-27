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
    public IEnumerator OpenCloseTablet()
    {
        if (_tabletEquiped && !_cooldown) // close
        {
            _tabletEquiped = false;
            _cooldown = true;
            _anim.Play("TabletClose");

            yield return new WaitForSeconds(1f);

            _crosshair1.enabled = false;
            _crosshair2.enabled = false;
            _targetLabel.enabled = true;

            _overlayUIManager.DisableDefaultInput();
            _overlayUIManager.HideCursor();

            _playerMovment.ReleaseMouseY();

            yield return new WaitForSeconds(1f);

            _cooldown = false;
        }
        else if (!_tabletEquiped && !_cooldown) // open
        {
            _tabletEquiped = true;
            _cooldown = true;
            _playerMovment.FixMouseY();

            _anim.Play("TabletOpen");

            _crosshair1.enabled = false;
            _crosshair2.enabled = false;
            _targetLabel.enabled = false;

            yield return new WaitForSeconds(.5f);

            _overlayUIManager.EnableDefaultInput();
            _overlayUIManager.ShowCursor();

            yield return new WaitForSeconds(1f);

            _cooldown = false;
        }
    }

    private bool _tabletEquiped = false;

    [SerializeField] private Movment _playerMovment;

    [SerializeField] private OverlayUIManager _overlayUIManager;

    // player UI stuff
    [SerializeField] private Image _crosshair1;
    [SerializeField] private Image _crosshair2;
    [SerializeField] private TMP_Text _targetLabel;

    private Animator _anim;
}
