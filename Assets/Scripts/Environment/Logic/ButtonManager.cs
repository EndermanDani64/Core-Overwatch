using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour, IHoldInteractible
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private float interactionCooldown;

    [Space]

    public bool Enabled = true;

    public InteractibleEvent OnButtonPressed;
    public InteractibleEvent WhileButtonPressed;
    public InteractibleEvent OnButtonReleased;
    public bool CurrentlyHolding { get; set; } = false;


    private string key = "[RMB]";

    private Animator _animator;
    private const string ANIMATION_PRESS = "ButtonPress";
    private const string ANIMATION_RELEASE = "ButtonRelease";


    private float _cooldownCounter = 0f;

    public void StartInteraction()
    {
        if (!IsInteractible()) return;

        _animator.Play(ANIMATION_PRESS);
        Debug.Log("Started Interacting with Button");

        CurrentlyHolding = true;
        _cooldownCounter = interactionCooldown;

        OnButtonPressed?.Invoke();
    }

    public void BaseInteraction()
    {
        WhileButtonPressed?.Invoke();
    }

    public void EndInteraction()
    {
        _animator.Play(ANIMATION_RELEASE);

        CurrentlyHolding = false;

        OnButtonReleased?.Invoke();
    }

    public string LookingAtText()
    {
        if (Enabled) {
            return $"{key} {ValueStorage.INTERACTIBLE_LOOKINGATTEXT}";
        } else {
            return ValueStorage.INTERACTIBLE_NOTENABLED;
        }
    }

    public bool IsInteractible()
    {
        return 
            Vector3.Distance(Camera.main.gameObject.transform.position, transform.position) <= interactionDistance
            &&
            _cooldownCounter <= 0
            && !CurrentlyHolding
            && Enabled;
    }


    void Update()
    {
        if (_cooldownCounter > 0)
            _cooldownCounter -= Time.deltaTime;
    }

    void Start()
    {
        _animator = gameObject.GetComponentInParent<Animator>();
    }
}

