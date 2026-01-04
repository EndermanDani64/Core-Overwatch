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

    public float Stamina = 100f;
    [SerializeField] private TextMeshProUGUI StaminaText;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;

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

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2( // ee
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
    void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !HazmatSuit.isHazmat && Stamina > 0 && isGrounded) // regular sprint
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_NAKED_SPRINT;
            Stamina -= Time.deltaTime * ValueStorage.STAMINA_NAKED_WALK_DECREASE_MULTIPLIER;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && HazmatSuit.isHazmat && Stamina > 0) // hazmat sprint
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_HAZMAT_SPRINT; 
            Stamina -= Time.deltaTime * ValueStorage.STAMINA_HAZMAT_SPRINT_DECREASE_MULTIPLIER; // stamina csökkentés
        }
        else if (HazmatSuit.isHazmat && Mathf.Round(Stamina) >= 0) // hazmat walk
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_HAZMAT;

            if (Stamina < 100)
            {
                Stamina += Time.deltaTime * 5;
            }
        }
        else if (!HazmatSuit.isHazmat && Mathf.Round(Stamina) >= 0) // regular walk
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_NAKED; 

            if (Stamina < 100)
            {
                Stamina += Time.deltaTime * 5;
            }
        }

        Speed = Mathf.Lerp(Speed, targetSpeed, Time.deltaTime * 10);
        
        int roundedStamina = Mathf.RoundToInt(Stamina);
        if (roundedStamina != lastDisplayedStamina)
        {
            StaminaText.text = $"Stamina: {roundedStamina}";
            lastDisplayedStamina = roundedStamina;
        }
    }

}