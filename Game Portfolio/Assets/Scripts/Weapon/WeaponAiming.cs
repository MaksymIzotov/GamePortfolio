using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public float aimSpeed;

    public Vector3 hipPosition;
    public Vector3 aimPosition;

    private Vector3 target;

    private WeaponInfo info;

    [HideInInspector] public bool isMoving = false;

    private void Start()
    {
        info = GetComponent<WeaponInfo>();
    }

    void Update()
    {
        if (info.weaponState == WeaponInfo.State.PICKUP) { return; }

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
        if(info.weaponState == WeaponInfo.State.RELOAD)
        {
            target = hipPosition;
            isMoving = true;
            return;
        }
        Debug.Log(isMoving);

        if (Input.GetKey(InputManager.Instance.Aim))
            target = aimPosition;
        else
            target = hipPosition;

        if (Input.GetKeyDown(InputManager.Instance.Aim) || Input.GetKeyUp(InputManager.Instance.Aim))
            isMoving = true;
    }

    public void AfterReloadMovingState()
    {
        isMoving = true;
    }
}
