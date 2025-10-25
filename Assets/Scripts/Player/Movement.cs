using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] public float mouseSensitivity = 3.5f;
    [SerializeField] public float Speed = 4.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -9.8f; // -30f
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
        //StaminaText = GetComponent<TextMeshProUGUI>();
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (HazmatSuit.isHazmat && Speed != ValueStorage.PLAYER_SPEED_HAZMAT) // changing the speed of the player when the hazmat suit is worn
        {
            Speed = ValueStorage.PLAYER_SPEED_HAZMAT;
        }
        else if (!HazmatSuit.isHazmat && Speed != ValueStorage.PLAYER_SPEED_NAKED)
        {
            Speed = ValueStorage.PLAYER_SPEED_NAKED;
        }
        HandleStamina();
        UpdateMouse();
        UpdateMove();
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
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground); // megnézzük hogy a groundCheck érintkezik e a "ground" taggel rendelkezõ GameObject-el (a földel)

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize(); // ne gyorsuljunk attól hogy A + W nyomjuk

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY; // mozgás kiszámolása és 

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
    void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !HazmatSuit.isHazmat && Stamina > 0 && isGrounded)
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_NAKED_SPRINT; // sprint speed
            //Speed = ValueStorage.PLAYER_SPEED_NAKED_SPRINT;
            Stamina -= Time.deltaTime * 8; // stamina csökkentés
        }
        else if (Input.GetKey(KeyCode.LeftShift) && HazmatSuit.isHazmat && Stamina > 0)
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_HAZMAT_SPRINT; // hazmat sprint speed
            //Speed = ValueStorage.PLAYER_SPEED_HAZMAT_SPRINT;
            Stamina -= Time.deltaTime * 15; // stamina csökkentés
        }
        else if (HazmatSuit.isHazmat && Stamina > 0)
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_HAZMAT; // hazmat speed
            //Speed = ValueStorage.PLAYER_SPEED_HAZMAT;
            Stamina -= Time.deltaTime * 15; // stamina csökkentés
        }
        else if (!HazmatSuit.isHazmat && Stamina > 0)
        {
            targetSpeed = ValueStorage.PLAYER_SPEED_NAKED; // walking speed
            //Speed = ValueStorage.PLAYER_SPEED_NAKED;
            if (Stamina < 100)
            {
                Stamina += Time.deltaTime * 5; // Regeneráció, ha nincs sprint
            }
        }

        // Fokozatos sebességváltás
        Speed = Mathf.Lerp(Speed, targetSpeed, Time.deltaTime * 10);
        StaminaText.text = $"Stamina: {Stamina}";
        Debug.Log($"Stamina: {Stamina}");
    }
}

