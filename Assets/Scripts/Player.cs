using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private GameObject _cube1;

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
        transform.Rotate(0, 15f, 0);
        float deg = transform.localEulerAngles.y;
        Debug.Log("deg: " + deg);

        Vector3 diff = _cube1.transform.position - transform.position;
        float rad = Mathf.Atan2(diff.x, diff.z);
        float deg2 = rad * Mathf.Rad2Deg;
        Debug.Log("diff: " + diff);
        Debug.Log("rad: " + rad + " deg2: " + deg2);
    }

}
