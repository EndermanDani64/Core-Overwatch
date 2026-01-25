using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    private void Awake()
    {
        _camera = Camera.main.transform;
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
        _as = GetComponentInChildren<AudioSource>();
    }

    /// <summary>
    /// Calculates a Vector3 that is going to be applied to player's camera.
    /// </summary>
    /// <returns>Player's camera offset while moving.</returns>
    private Vector3 StepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.x += Mathf.Sin(_bobTimer * _xOffsetValue) * _amplitude;
        pos.y += Mathf.Cos(_bobTimer * _yOffsetValue) * _amplitude;
        return pos;
    }

    private void ApplyOffset()
    {
        if (!_controller.isGrounded) { return; }

        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;

        if (speed < _toggleSpeed) { _bobTimer = 0; return; }

        _bobTimer += Time.deltaTime * speed;
        _camera.localPosition += StepMotion();

        if (Mathf.Round(_bobTimer) % 2 == 0 && !_as.isPlaying)
        {
            int randomFootstep = Random.Range(1, 6);
            switch (randomFootstep)
            {
                case 1:
                    _as.PlayOneShot(_metalFootstep1);
                    break;
                case 2:
                    _as.PlayOneShot(_metalFootstep2);
                    break;
                case 3:
                    _as.PlayOneShot(_metalFootstep3);
                    break;
                case 4:
                    _as.PlayOneShot(_metalFootstep4);
                    break;
                case 5:
                    _as.PlayOneShot(_metalFootstep5);
                    break;
                case 6:
                    _as.PlayOneShot(_metalFootstep6);
                    break;
            }
        }
    }

    /// <summary>
    /// Resets the player's camera in the _startPos with Lerp.
    /// Used for looping back the main camera to the _startPos after offset.
    /// </summary>
    private void ResetMotion()
    {
        if (_camera.localPosition == _startPos) { return; }

        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }
    
    void Update()
    {
        if (!enable) { return; }

        ApplyOffset();
        ResetMotion();
    }

    [SerializeField] public static bool enable = true;

    [SerializeField] private float _amplitude = 0.1f;

    private Transform _camera;
    [SerializeField] private Transform _cameraHolder;

    [SerializeField] private float _toggleSpeed = 2f;
    private float _bobTimer = 0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    [SerializeField] private float _xOffsetValue = 0.25f;
    [SerializeField] private float _yOffsetValue = 0.8f;

    private AudioSource _as;
    [SerializeField] private AudioClip _metalFootstep1;
    [SerializeField] private AudioClip _metalFootstep2;
    [SerializeField] private AudioClip _metalFootstep3;
    [SerializeField] private AudioClip _metalFootstep4;
    [SerializeField] private AudioClip _metalFootstep5;
    [SerializeField] private AudioClip _metalFootstep6;
}
