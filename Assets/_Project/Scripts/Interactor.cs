using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private InputActions _input;
    private Camera _camera;

    private IInteractable _interactable = null;


    [Tooltip("Слой объектов, с которыми можно взаимодействовать")]
    [SerializeField] private LayerMask interactableLayer;

    [Tooltip("Дальность проверки взгляда")]
    public float maxDistance = 5f;


    private Transform _hitTransform = null;

    public void Initialize(InputActions input)
    {
        _input = input;
        _input.Player.Interact.started += OnInteractStarted;

        _camera = Camera.main;
    }

    private void Update()
    {
        CheckInteractable();
    }


    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        if (_interactable != null)
        {
            _interactable.Interact(_input);
        }
    }

    private void CheckInteractable()
    {
        Vector3 rayOrigin = _camera.transform.position;
        Vector3 rayDirection = _camera.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxDistance, interactableLayer))
        {
            UpdateTransform(hit.transform);
        }
        else
        {
            _interactable?.Unhighlight();
            _interactable = null;
            _hitTransform = null;
        }
    }

    private void UpdateTransform(Transform transform)
    {
        if (transform != _hitTransform)
        {
            if (transform.TryGetComponent<Interactable>(out Interactable interactable))
            {
                _interactable = interactable;
                if (_interactable.IsAvaliable)
                {
                    _interactable.Highlight();
                }
                else
                {
                    _interactable.Unhighlight();
                }
            }
        }
    }

    private void OnDestroy()
    {
        _input.Player.Interact.started -= OnInteractStarted;
    }
}
