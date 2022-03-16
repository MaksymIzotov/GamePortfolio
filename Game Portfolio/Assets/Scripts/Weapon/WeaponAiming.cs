using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public float aimSpeed;

    public Transform hipPosition;
    public Transform aimPosition;

    private Transform target;


    void Start()
    {
        
    }

    void Update()
    {
        HandleInput();
        UpdateLocation();
    }

    void UpdateLocation()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.001f)
        {
            float step = aimSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    void HandleInput()
    {
        if (Input.GetKey(InputManager.Instance.Aim))
            target = aimPosition;
        else
            target = hipPosition;
    }

    public bool AimingState => Vector3.Distance(transform.position, hipPosition.position) < 0.001f ? false : true;
}
