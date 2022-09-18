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
    private GameObject _cam;
    private CharacterController _characon;
    private Vector2 _move_direction;
    private float _target_angle = 0f;
    private float _current_velocity = 0f;

    void Awake()
    {
        TryGetComponent(out _input);
        TryGetComponent(out _characon);
        _cube1 = GameObject.Find("Cube1");
        _cam = GameObject.Find("Main Camera");
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
        _move_direction = obj.ReadValue<Vector2>();
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
        if (_move_direction != Vector2.zero)
        {
            _target_angle = Mathf.Atan2(_move_direction.x, _move_direction.y) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
        }
        float angle = Mathf.SmoothDampAngle(current, _target_angle, ref _current_velocity, _smooth_time);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        if (_move_direction != Vector2.zero)
        {
            Vector3 direction = Quaternion.Euler(0, _target_angle, 0) * Vector3.forward;
            _characon.Move(direction.normalized * _speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        Move();
    }

}
