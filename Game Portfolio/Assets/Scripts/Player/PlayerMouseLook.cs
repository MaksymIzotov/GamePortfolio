using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMouseLook : MonoBehaviourPunCallbacks
{
    #region Public Variables

    #endregion

    #region Private Variables

    private Transform playerCam;

    private float mouseX;
    private float mouseY;
    private float desiredX;
    private float xRotation;
    private Vector3 rotation;


    private float headJump = 0;

    private int sensMult = 10;

    CharacterController cc;

    #endregion

    #region Unity Methods

    void Start()
    {
        VariablesAssignment();

        GetComponent<PlayerMouseLook>().enabled = ErrorHandling();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseMovement();
    }
    #endregion

    #region Created Methods

    void MouseMovement()
    {
        mouseX = Input.GetAxis("Mouse X") * InputManager.Instance.sensitivity * Time.fixedDeltaTime * sensMult;
        mouseY = Input.GetAxis("Mouse Y") * InputManager.Instance.sensitivity * Time.fixedDeltaTime * sensMult;

        rotation = transform.localRotation.eulerAngles;
        desiredX = rotation.y + mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        headJump = Mathf.Clamp(Mathf.Lerp(headJump, cc.velocity.y,0.05f),-10,10);

        playerCam.localRotation = Quaternion.Euler(xRotation + headJump, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }



    #endregion

    #region Technical Methods

    void VariablesAssignment()
    {
        playerCam = GetComponentInChildren<Camera>().transform;
        cc = GetComponent<CharacterController>();
    }

    bool ErrorHandling()
    {
        if (playerCam == null)
        {
            ErrorHandler.Instance.GameObjectIsMissing("Camera");
            return false;
        }
        return true;
    }

    #endregion
}
