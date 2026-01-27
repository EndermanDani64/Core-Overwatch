using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

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

            _playerMovment.ReleaseMouseY();

            yield return new WaitForSeconds(1.5f);

            _cooldown = false;
        }
        else if (!_tabletEquiped && !_cooldown) // open
        {
            _tabletEquiped = true;
            _cooldown = true;
            _anim.Play("TabletOpen");
            _playerMovment.FixMouseY();

            yield return new WaitForSeconds(1.5f);

            _cooldown = false;
        }
    }

    private bool _tabletEquiped = false;

    [SerializeField] private Movment _playerMovment;

    private Animator _anim;
}
