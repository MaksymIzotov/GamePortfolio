using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public float aimSpeed;

    public Transform hipPosition;
    public Transform aimPosition;

    private Transform target;


    [HideInInspector] public bool isMoving = false;

    void Update()
    {
        HandleInput();
        UpdateLocation();
    }

    void UpdateLocation()
    {
        if (!isMoving) { return; }

        if (Vector3.Distance(transform.position, target.position) < 0.001f) { isMoving = false; return; }

        float step = aimSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void HandleInput()
    {
        if (Input.GetKey(InputManager.Instance.Aim))
            target = aimPosition;
        else
            target = hipPosition;

        if (Input.GetKeyDown(InputManager.Instance.Aim) || Input.GetKeyUp(InputManager.Instance.Aim))
            isMoving = true;
    }
}
