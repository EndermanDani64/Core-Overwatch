using TMPro;
using UnityEngine;

public class Movment : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    // [SerializeField] bool cursorLock = true;
    [SerializeField] public float mouseSensitivity = 3.5f;
    [SerializeField] public float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    private bool _camFixedY = false;

    [SerializeField] private TextMeshProUGUI StaminaText;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    float _cameraPitch;
    float _cameraPitchVelocity;
    [SerializeField] float _smoothTime = 0.15f;
    float _tabletTargetPitch = 0f;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!OverlayUIManager.isPaused)
        {
            HandleStamina();
            UpdateMouse();
            UpdateMove();
        }
    }

    private void UpdateMouse()
    {
        if (!_camFixedY)
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // were the player is looking at

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            _cameraPitch -= currentMouseDelta.y * mouseSensitivity;

            _cameraPitch = Mathf.Clamp(_cameraPitch, -55.0f, 55.0f);

            playerCamera.localEulerAngles = Vector3.right * _cameraPitch;

            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }
        else
        {
            Vector2 targetMouseDelta = new Vector2(0f, 0f);

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            _cameraPitch = Mathf.SmoothDamp(_cameraPitch, _tabletTargetPitch, ref _cameraPitchVelocity, _smoothTime);

            playerCamera.localEulerAngles = Vector3.right * _cameraPitch;

            transform.Rotate(Vector3.up * 0f * mouseSensitivity);
        }
    }

    public void FixMouseY()
    {
        _camFixedY = true;
    }

    public void ReleaseMouseY()
    {
        _camFixedY = false;
    }

    private void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

        Vector2 targetDir = new Vector2( 
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    [SerializeField] public float targetSpeed = ValueStorage.PLAYER_SPEED_NAKED; // alap sebesség
    int lastDisplayedStamina = -1;
    private void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !HazmatSuit.isHazmat && Player.stamina > 0 && isGrounded) // regular sprint
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_NAKED_SPRINT;
            Player.stamina -= Time.deltaTime * ValueStorage.STAMINA_NAKED_WALK_DECREASE_MULTIPLIER;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && HazmatSuit.isHazmat && Player.stamina > 0) // hazmat sprint
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_HAZMAT_SPRINT;
            Player.stamina -= Time.deltaTime * ValueStorage.STAMINA_HAZMAT_SPRINT_DECREASE_MULTIPLIER; // stamina csökkentés
        }
        else if (HazmatSuit.isHazmat && Mathf.Round(Player.stamina) >= 0) // hazmat walk
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_HAZMAT;

            if (Player.stamina < 100)
            {
                Player.stamina += Time.deltaTime * 5;
            }
        }
        else if (!HazmatSuit.isHazmat && Mathf.Round(Player.stamina) >= 0) // regular walk
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_NAKED; 

            if (Player.stamina < 100)
            {
                Player.stamina += Time.deltaTime * 5;
            }
        }

        Speed = Mathf.Lerp(Speed, targetSpeed, Time.deltaTime * 10);
        
        int roundedStamina = Mathf.RoundToInt(Player.stamina);
        if (roundedStamina != lastDisplayedStamina)
        {
            StaminaText.text = $"Stamina: {roundedStamina}";
            lastDisplayedStamina = roundedStamina;
        }
    }

}