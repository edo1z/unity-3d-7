using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput _input;

    void Awake()
    {
        TryGetComponent(out _input);
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
        transform.Rotate(0, 15f, 0);
    }

}
