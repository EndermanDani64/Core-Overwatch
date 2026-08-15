using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SliderManager : MonoBehaviour, IHoldInteractible
{

    [Header("Slider Variables")]
    [Range(float.MinValue, float.MaxValue)]
    public float Value;
    public float MaxValue;
    public float MinValue;
    public InteractibleEvent OnValueChanged;

    [Space][Space]
    [Header("Other Slider Variables")]
    [SerializeField] private float slidingSpeedMultiplier = 2f;
    [SerializeField] private float interactionDistance;
    [SerializeField] private float interactionCooldown;
    public bool CurrentlyHolding { get; set; } = false;

    
    private float _handleTComponent;
    private float3 _evaluatedHandleTargetPos;

    public void StartInteraction()
    {
        if (!IsInteractible()) return;

        BaseInteraction();

        CurrentlyHolding = true;
    }


    // Here at the slider, the main logic is stored in BaseInteraction()
    // and because of the sliders nature the same base logic needs to be called when starting and ending the interaction.
    public void BaseInteraction()
    {
        if (!IsInteractible()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out RaycastHit raycastHit) && raycastHit.collider.gameObject.CompareTag("SliderHandleSpace"))
        {
            Vector3 localTargetPosition = splineContainer.transform.InverseTransformPoint(raycastHit.point);

            SplineUtility.GetNearestPoint(splineContainer[0], localTargetPosition, out _, out _handleTComponent, 10, 10);

            _evaluatedHandleTargetPos = splineContainer.EvaluatePosition(_handleTComponent);
        }

        MoveSliderHandle();
        UpdateSliderValue();
    }


    public void EndInteraction()
    {
        if (!IsInteractible()) return;

        BaseInteraction();

        CurrentlyHolding = false;

    }


    public string LookingAtText()
    {
        return "Hold [RMB] for adjust the slider";
    }

    public bool IsInteractible()
    {
        return
            Vector3.Distance(Camera.main.gameObject.transform.position, transform.position) <= interactionDistance
            &&
            interactionCooldown <= 0;
    }


    // -- sub -- //


    private void MoveSliderHandle()
    {
        sliderHandle.transform.position = Vector3.MoveTowards(sliderHandle.transform.position, _evaluatedHandleTargetPos, Time.deltaTime * slidingSpeedMultiplier);
    }

    private float _lastValue;
    private void UpdateSliderValue()
    {
        Value = Mathf.Lerp(MinValue, MaxValue, Mathf.Max(0, _handleTComponent));

        if (_lastValue != Value)
        {
            _lastValue = Value;
            OnValueChanged?.Invoke();
        }
    }


    // -- ref -- //


    [Space][Space][Space]
    [Header("References")]
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private GameObject sliderHandle;
}


