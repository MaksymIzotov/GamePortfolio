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
    [Tooltip("Weapon breathe amount")]
    public float bobbingAmount = 0;

    #endregion

    #region Private Variables

    private float defaultPosY;

    private float timer;

    private WeaponAiming wa;

    #endregion

    #region Unity Methods
    void Start()
    {
        VariablesAssignment();
    }


    void Update()
    {
        BreatheMovement();
    }

    #endregion

    #region Created Methods

    void BreatheMovement()
    {
        if (wa.AimingState) { timer = 0; return; }

        timer += Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
    }

    #endregion

    #region Technical Methods

    void VariablesAssignment()
    {
        wa = GetComponent<WeaponAiming>();
        if(wa == null)
            ErrorHandler.Instance.GameObjectIsMissing("Weapon Aiming Component");

        if (cam == null)          
            ErrorHandler.Instance.GameObjectIsMissing("Camera");

        defaultPosY = transform.localPosition.y;
        timer = 0;
    }

    #endregion
}
