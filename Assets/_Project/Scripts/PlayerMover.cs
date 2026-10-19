using System;
using UnityEngine;
using UnityEngine.Windows;


public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _controller;
    private InputActions _input;
    private Transform _camera;

    private float _lookDownLimit = -90f;
    private float _lookUpLimit = 90f;
    private float _xRotation = 0f;

    private Vector3 _velocity;
    private bool _isGrounded;

    private PlayerData _data;

    public void Initialize(InputActions input, PlayerData data)
    {
        _input = input;
        _data = data;
    }

    private void Awake()
    {
        _camera = Camera.main.transform;
        _controller = GetComponent<CharacterController>();
    }



    void Update()
    {
        // Проверка, стоит ли персонаж на земле
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // небольшое прижатие к земле, чтобы не «плавал»
        }

        // Обработка прыжка
        if (_input.Player.Jump.WasPerformedThisFrame() && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_data.JumpHeight * -2f * _gravity);
        }

        // Ввод движения
        Vector2 input = _input.Player.Move.ReadValue<Vector2>();
        Rotate();

        if (input == Vector2.zero)
            return;

        // Направления относительно поворота персонажа
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Движение вперёд/назад + стрейф
        Vector3 moveDirection = (forward * input.y + right * input.x) * GetSpeed();



        // Применяем гравитацию
        _velocity.y += _gravity * Time.deltaTime;

        // Двигаем персонажа: горизонтальное движение + вертикальная скорость (гравитация и прыжок)
        moveDirection.y = _velocity.y;
        _controller.Move(moveDirection * Time.deltaTime);
    }

    public void Rotate()
    {
        Vector2 lookInput = _input.Player.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * _data.MouseSensitivity;
        float mouseY = lookInput.y * _data.MouseSensitivity;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _lookDownLimit, _lookUpLimit);

        _camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private float GetSpeed()
    {
        bool isSprint = _input.Player.Sprint.IsPressed();

        return isSprint ? _data.Speed * _data.RunSpeed : _data.Speed;
    }
}
