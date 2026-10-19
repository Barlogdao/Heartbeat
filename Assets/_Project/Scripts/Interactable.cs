using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    private Color _normalColor;
    [SerializeField] private Color _highlightColor;

    private SphereCollider _triggerZone;

    private Material _material;
    protected bool IsBusy = false;

    public bool IsAvaliable => !IsBusy;

    private void Awake()
    {
        _triggerZone = GetComponent<SphereCollider>();
        _triggerZone.isTrigger = true;
        _material = GetComponent<Renderer>().material;
        _normalColor = _material.GetColor("_BaseColor");

        OnAwake();
    }

    protected virtual void OnAwake() { }

    public abstract void Interact(InputActions input);

    public void Highlight()
    {
        if (IsBusy)
            return;

        _material.SetColor("_BaseColor", _highlightColor);
    }

    public void Unhighlight()
    {
        _material.SetColor("_BaseColor", _normalColor);
    }
}
