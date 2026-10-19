using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTablet : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Renderer _TV;
    [SerializeField] private Texture _originTexture;

    private Material _material;
    private List<RenderTexture> _camerasTextures = new();

    private bool _isShowed = false;
    private int _currentIndex = -1;
    private InputActions _input;

    public void Initialize(InputActions input)
    {
        _input = input;
        if (_input == null)
        {
            Debug.LogWarning("[CameraTablet] InputActions is null!");
            return;
        }

        _input.Player.Tablet.performed += OnTabletPressed;
        _input.Player.Next.performed += OnNextPressed;
        _input.Player.Previous.performed += OnPreviousPressed;
    }

    private void OnDestroy()
    {
        if (_input == null) return;
        _input.Player.Tablet.performed -= OnTabletPressed;
        _input.Player.Next.performed -= OnNextPressed;
        _input.Player.Previous.performed -= OnPreviousPressed;
    }

    private void Awake()
    {
        if (_TV == null)
        {
            Debug.LogError("[CameraTablet] _TV is not assigned!");
            enabled = false;
            return;
        }

        _material = _TV.material;
    }

    private void OnTabletPressed(InputAction.CallbackContext context)
    {
        _isShowed = !_isShowed;

        if (_isShowed)
        {
            if (_animator != null) _animator.SetBool("IsShown", true);

            if (_camerasTextures.Count > 0)
            {
                _currentIndex = 0;
                SetCamera(_camerasTextures[_currentIndex]);
            }
            else
            {
                _currentIndex = -1;
                SetCamera(null);
            }
        }
        else
        {
            if (_animator != null) _animator.SetBool("IsShown", false);
            _currentIndex = -1;
        }
    }

    private void OnNextPressed(InputAction.CallbackContext context)
    {
        if (!_isShowed || _camerasTextures.Count == 0) return;

        _currentIndex = (_currentIndex + 1) % _camerasTextures.Count;
        SetCamera(_camerasTextures[_currentIndex]);
    }

    private void OnPreviousPressed(InputAction.CallbackContext context)
    {
        if (!_isShowed || _camerasTextures.Count == 0) return;

        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = _camerasTextures.Count - 1;

        SetCamera(_camerasTextures[_currentIndex]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.TryGetComponent<CameraPoint>(out var cameraPoint))
        {
            _camerasTextures = cameraPoint.GetCameras();

            _currentIndex = -1;

            if (_camerasTextures.Count > 0)
            {
                _currentIndex = 0;
                SetCamera(_camerasTextures[_currentIndex]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) return;
        if (other.TryGetComponent<CameraPoint>(out var cameraPoint))
        {
            _camerasTextures = null;
            _currentIndex = -1;

            if (_isShowed)
                SetCamera(null);

            Debug.Log("[CameraTablet] Trigger exit. List cleared.");
        }
    }

    private void SetCamera(RenderTexture texture)
    {
        if (texture == null)
        {
            _material.SetTexture("_BaseMap", _originTexture);
            return;
        }
        _material.SetTexture("_BaseMap", texture);
    }
}
