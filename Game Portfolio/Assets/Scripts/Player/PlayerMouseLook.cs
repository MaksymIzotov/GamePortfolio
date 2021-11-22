using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    #region Public Variables

    public float sensitivity;

    #endregion

    #region Private Variables

    private Transform playerCam;

    private float mouseX;
    private float mouseY;
    private float desiredX;
    private float xRotation;
    private Vector3 rotation;

    private int sensMult = 10;

    #endregion

    #region Unity Methods

    void Start()
    {
        playerCam = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        MouseMovement();
    }
    #endregion

    #region Created Methods

    void MouseMovement()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMult;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMult;

        rotation = playerCam.transform.localRotation.eulerAngles;
        desiredX = rotation.y + mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.localRotation = Quaternion.Euler(xRotation,desiredX, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    #endregion
}
