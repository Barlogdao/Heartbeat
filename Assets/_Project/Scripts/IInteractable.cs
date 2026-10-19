public interface IInteractable
{
    public void Interact(InputActions input);

    public void Highlight();
    public void Unhighlight();

    public bool IsAvaliable {  get; }
}


