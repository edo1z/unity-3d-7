using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _smooth_time = 0.3f;
    [SerializeField] private float _speed = 5f;

    private PlayerInput _input;
    private GameObject _cube1;
    private CharacterController _characon;
    private Vector3 _move_direction;
    private float _target_angle = 0f;
    private float _current_velocity = 0f;

    void Awake()
    {
        TryGetComponent(out _input);
        TryGetComponent(out _characon);
        _cube1 = GameObject.Find("Cube1");
    }

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMove;
        _input.actions["Rotate"].started += OnRotate;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMove;
        _input.actions["Rotate"].started -= OnRotate;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        Vector2 direction = obj.ReadValue<Vector2>();
        _move_direction = new Vector3(direction.x, 0f, direction.y).normalized;
        if (_move_direction != Vector3.zero)
        {
            _target_angle = Mathf.Atan2(_move_direction.x, _move_direction.z) * Mathf.Rad2Deg;
        }
    }

    private void OnRotate(InputAction.CallbackContext obj)
    {
        _target_angle += 90f;
        if (_target_angle > 360)
        {
            _target_angle -= 360f;
        }
    }

    private void Move()
    {
        float current = transform.eulerAngles.y;
        float deg = Mathf.SmoothDampAngle(current, _target_angle, ref _current_velocity, _smooth_time);
        transform.rotation = Quaternion.Euler(0, deg, 0);
        _characon.Move(_move_direction * _speed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }

}
