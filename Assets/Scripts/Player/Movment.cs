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

    float targetSpeed = 4; // alap sebesség
    void HandleStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && isGrounded)
        {
            Debug.LogWarning("eee");
            targetSpeed = 18; // sprint sebesség
            Stamina -= Time.deltaTime * 10; // stamina csökkentés
        }
        else
        {
            targetSpeed = 4; // Alap sebesség
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

