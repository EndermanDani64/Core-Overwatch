using UnityEngine;

public class HeadBob : MonoBehaviour
{
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

    private void Awake()
    {
        _camera = Camera.main.transform;
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
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

        if (speed < _toggleSpeed) { return; }

        _bobTimer += Time.deltaTime * speed;
        _camera.localPosition += StepMotion();
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
}
