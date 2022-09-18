using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _smooth_time = 0.3f;

    private PlayerInput _input;
    private GameObject _cube1;
    private float _target_angle = 0f;
    private float _current_velocity = 0f;

    void Awake()
    {
        TryGetComponent(out _input);
        _cube1 = GameObject.Find("Cube1");
    }

    private void OnEnable()
    {
        _input.actions["Rotate"].started += Rotate;
    }

    private void OnDisable()
    {
        _input.actions["Rotate"].started -= Rotate;
    }

    private void Rotate(InputAction.CallbackContext obj)
    {
        _target_angle += 90f;
        if (_target_angle > 360) {
          _target_angle -= 360f;
        }

        Vector3 diff = _cube1.transform.position - transform.position;
        float rad = Mathf.Atan2(diff.x, diff.z);
        float deg2 = rad * Mathf.Rad2Deg;
        Debug.Log("diff: " + diff);
        Debug.Log("rad: " + rad + " deg2: " + deg2);
    }

    private void Update()
    {
        float current = transform.eulerAngles.y;
        float deg = Mathf.SmoothDampAngle(current, _target_angle, ref _current_velocity, _smooth_time);
        transform.rotation = Quaternion.Euler(0, deg, 0);
    }

}
