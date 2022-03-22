using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCameraFollow : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Amount of weapon sway to be applied")]
    public float swayAmount = 0;

    #endregion

    #region Private Variables

    private float mouseX;
    private float mouseY;

    private PlayerMouseLook mouseSettings;

    #endregion

    #region Unity Methods
    void Start()
    {
        VariablesAssignment();
    }


    void Update()
    {
        WeaponSway();
    }

    #endregion

    #region User Methods

    void WeaponSway()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSettings.sensitivity * Time.fixedDeltaTime * swayAmount;
        mouseY = Input.GetAxis("Mouse Y") * mouseSettings.sensitivity * Time.fixedDeltaTime * swayAmount;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(Mathf.Clamp(-mouseY * 5, -45, 45), Mathf.Clamp(mouseX * 5, -45, 45), 0)), 0.07f);
    }

    #endregion

    #region Technical Methods

    void VariablesAssignment()
    {
        mouseSettings = GameObject.Find("Player").GetComponent<PlayerMouseLook>();

        if (mouseSettings == null)
            ErrorHandler.Instance.GameObjectIsMissing("Mouse look script on player");
    }

    #endregion
}
