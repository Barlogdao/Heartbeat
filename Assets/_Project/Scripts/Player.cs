using UnityEngine;

public class Player : MonoBehaviour
{
    private InputActions _input;

    [SerializeField] private Interactor _interactor;
    [SerializeField] private PlayerData _data;
    [SerializeField] private CameraTablet _tablet;
    
    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = new InputActions();


        PlayerMover mover = GetComponent<PlayerMover>();
        mover.Initialize(_input,_data);

        _interactor.Initialize(_input);
        _tablet.Initialize(_input);
    }

    private void OnEnable() => _input.Enable();

    private void OnDisable() => _input.Disable();
}
