using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public float aimSpeed;

    public Vector3 hipPosition;
    public Vector3 aimPosition;

    private Vector3 target;


    [HideInInspector] public bool isMoving = false;
    void Update()
    {
        HandleInput();
        UpdateLocation();
    }

    void UpdateLocation()
    {
        if (!isMoving) { return; }

        if (Vector3.Distance(transform.localPosition, target) < 0.001f) { isMoving = false; return; }

        float step = aimSpeed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, step);
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
