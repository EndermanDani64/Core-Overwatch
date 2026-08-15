public interface IInteractible 
{
    void Interact(); // ezt hívnám meg ha Input.GetKey(InteractionButton) <- eddig ezt használom
    string LookingAtText();
    bool IsInteractible();
}