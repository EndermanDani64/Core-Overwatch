public interface IHoldInteractible
{
    void StartInteraction(); // ezt hívnám meg ha Input.GetKeyDown(InteractionButton) <- nincs még használatban (új)
    void BaseInteraction(); // ezt hívnám meg ha Input.GetKey(InteractionButton) <- eddig ezt használom
    void EndInteraction(); // ezt hívnám meg ha Input.GetKeyUp(InteractionButton) <- nincs még használatban (új)
    string LookingAtText();
    bool IsInteractible();
    bool CurrentlyHolding { get; set; }
}
