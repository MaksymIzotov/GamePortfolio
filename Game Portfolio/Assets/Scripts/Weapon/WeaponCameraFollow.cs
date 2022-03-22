using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCameraFollow : MonoBehaviour
{
    #region Public Variables

    [Tooltip("Speed of weapon following the camera")]
    public float followSpeed = 1f;
    [Tooltip("Weapon breathe amount when hipfire")]
    public float bobbingAmountAim = 0;
    [Tooltip("Weapon breathe amount when aiming")]
    public float bobbingAmountHip = 0;
    [Tooltip("Amount of weapon sway to be applied")]
    public float swayAmount = 0;

    #endregion

    #region Private Variables

    private float defaultPosY;

    private float timer;
    private float moveY;

    private GameObject cam;
    private float mouseX;
    private float mouseY;

    private WeaponAiming wa;
    private PlayerMouseLook mouseSettings;
    private WeaponInfo info;

    #endregion

    #region Unity Methods
    void Start()
    {
        VariablesAssignment();
    }


    void Update()
    {
        WeaponSway();

        if (wa.isMoving) { timer = 0; return; }

        HandleInput();
        BreatheMovement();
    }

    #endregion

    #region User Methods

    void BreatheMovement()
    {
        timer += Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * moveY, transform.localPosition.z);
    }

    void WeaponSway()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSettings.sensitivity * Time.fixedDeltaTime * swayAmount;
        mouseY = Input.GetAxis("Mouse Y") * mouseSettings.sensitivity * Time.fixedDeltaTime * swayAmount;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(Mathf.Clamp(-mouseY * 5, -45, 45), Mathf.Clamp(mouseX * 5, -45, 45), 0)), 0.07f);
    }

    #endregion

    #region Technical Methods

    void HandleInput()
    {
        if (info.weaponState == WeaponInfo.State.RELOAD) { SetHipMovement(); return; }

        if (Input.GetKeyDown(InputManager.Instance.Aim) || Input.GetKeyUp(InputManager.Instance.Aim)) { wa.isMoving = true; return; }

        if (Input.GetKey(InputManager.Instance.Aim))
            SetAimMovement();
        else
            SetHipMovement();
    }

    void SetAimMovement()
    {
        defaultPosY = wa.aimPosition.y;
        moveY = bobbingAmountAim;
    }

    void SetHipMovement()
    {
        defaultPosY = wa.hipPosition.y;
        moveY = bobbingAmountHip;
    }

    void VariablesAssignment()
    {
        wa = GetComponent<WeaponAiming>();
        cam = GameObject.Find("PlayerCamera");
        mouseSettings = GameObject.Find("Player").GetComponent<PlayerMouseLook>();
        info = GetComponent<WeaponInfo>();

        if (info == null)
            ErrorHandler.Instance.GameObjectIsMissing("Weapon Info Script");

        if (mouseSettings == null)
            ErrorHandler.Instance.GameObjectIsMissing("Mouse look script on player");

        if (wa == null)
            ErrorHandler.Instance.GameObjectIsMissing("Weapon Aiming Component");

        if (cam == null)          
            ErrorHandler.Instance.GameObjectIsMissing("Camera");

        timer = 0;
    }

    #endregion
}
