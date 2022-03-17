using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCameraFollow : MonoBehaviour
{
    #region Public Variables

    [Tooltip("Speed of weapon following the camera")]
    public float followSpeed = 1f;
    [Tooltip("Camera to be followed")]
    public GameObject cam;
    [Tooltip("Weapon breathe amount when hipfire")]
    public float bobbingAmountAim = 0;
    [Tooltip("Weapon breathe amount when aiming")]
    public float bobbingAmountHip = 0;

    #endregion

    #region Private Variables

    private float defaultPosY;

    private float timer;
    private float moveY;

    private WeaponAiming wa;

    #endregion

    #region Unity Methods
    void Start()
    {
        VariablesAssignment();
    }


    void Update()
    {
        if (wa.isMoving) { timer = 0; return; }

        HandleInput();
        BreatheMovement();
    }

    #endregion

    #region Created Methods

    void BreatheMovement()
    {
        timer += Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * moveY, transform.localPosition.z);
    }

    #endregion

    #region Technical Methods

    void HandleInput()
    {
        if (Input.GetKeyDown(InputManager.Instance.Aim) || Input.GetKeyUp(InputManager.Instance.Aim)) { wa.isMoving = true; return; }

        if (Input.GetKey(InputManager.Instance.Aim))
            SetAimMovement();
        else
            SetHipMovement();
    }

    void SetAimMovement()
    {
        defaultPosY = wa.aimPosition.localPosition.y;
        moveY = bobbingAmountAim;
    }

    void SetHipMovement()
    {
        defaultPosY = wa.hipPosition.localPosition.y;
        moveY = bobbingAmountHip;
    }

    void VariablesAssignment()
    {
        wa = GetComponent<WeaponAiming>();
        if(wa == null)
            ErrorHandler.Instance.GameObjectIsMissing("Weapon Aiming Component");

        if (cam == null)          
            ErrorHandler.Instance.GameObjectIsMissing("Camera");

        timer = 0;
    }

    #endregion
}
