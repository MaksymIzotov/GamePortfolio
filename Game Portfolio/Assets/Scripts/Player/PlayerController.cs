using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    #region Public Variables

    [Tooltip("Multiplier of walking speed")]
    public float walkSpeed = 5f;
    [Tooltip("Multiplier of running speed")]
    public float runSpeed = 8f;
    [Tooltip("Gravity forces that applies to the player")]
    public float gravity = 19.6f;
    [Tooltip("Amount of force will apply to the player when jumping")]
    public float jumpForce = 5f;
    [Tooltip("Height of ducking")]
    public float crouchHeight = 0.5f;
    [Tooltip("Time to duck")]
    public float crouchTime = 1f;
    [Tooltip("A step to change speed between walking and running")]
    public float speedChangingStep = 0.5f;


    #endregion

    #region Private Variables
    private PhotonView PV;
    private CharacterController cc;

    private Vector3 playerVelocity;
    private Vector3 movement;

    //[HideInInspector]
    public float speed;

    private float currentHeight;

    private float moveX;
    private float moveY;
    private bool isJumping;
    private bool isCrouching;
    [HideInInspector]
    public bool isRunning;
    private bool isGrounded;

    private Transform playerCam;

    #endregion

    #region Unity Methods

    void Start()
    {
        VariablesAssignment();

        GetComponent<PlayerController>().enabled = ErrorHandling();

        currentHeight = cc.height;
        speed = 0;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) { return; }

        MovePlayer();
    }

    private void Update()
    {
        if (!PV.IsMine) { return; }

        HandleInput();
        Crouch();
        Jump();
    }

    #endregion

    #region Created Methods

    void MovePlayer()
    {
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        movement = Quaternion.Euler(0, playerCam.transform.eulerAngles.y, 0) * new Vector3(moveX, 0, moveY);
        movement = Vector3.ClampMagnitude(movement, 1);

        SpeedHandle();

        cc.Move(movement * speed * cc.height * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
        isGrounded = cc.isGrounded;
    }

    void SpeedHandle()
    {
        if (isRunning)
            speed = Mathf.Lerp(speed, runSpeed, speedChangingStep / 3);
        else 
            speed = Mathf.Lerp(speed, walkSpeed, speedChangingStep / 3);
    }

    void Crouch()
    {
        if (isCrouching)
            cc.height = Mathf.Lerp(cc.height, crouchHeight, crouchTime);
        else if (CheckHeight())
            cc.height = Mathf.Lerp(cc.height, currentHeight, crouchTime);
    }

    void Jump()
    {
        if (isJumping && isGrounded)
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
    }

    void HandleInput()
    {
        if (Input.GetKey(InputManager.Instance.Forward))
            moveY = 1;
        if (Input.GetKey(InputManager.Instance.Backward))
            moveY = -1;
        if (Input.GetKey(InputManager.Instance.Forward) && Input.GetKey(InputManager.Instance.Backward) || !Input.GetKey(InputManager.Instance.Forward) && !Input.GetKey(InputManager.Instance.Backward))
            moveY = 0;

        if (Input.GetKey(InputManager.Instance.Right))
            moveX = 1;
        if (Input.GetKey(InputManager.Instance.Left))
            moveX = -1;
        if (Input.GetKey(InputManager.Instance.Left) && Input.GetKey(InputManager.Instance.Right) || !Input.GetKey(InputManager.Instance.Left) && !Input.GetKey(InputManager.Instance.Right))
            moveX = 0;

        isJumping = Input.GetKeyDown(InputManager.Instance.Jump);
        isRunning = Input.GetKey(InputManager.Instance.Run);
        isCrouching = Input.GetKey(InputManager.Instance.Crouch);
    }

    bool CheckHeight()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + cc.radius), transform.TransformDirection(Vector3.up), out hit, currentHeight))
            return false;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - cc.radius), transform.TransformDirection(Vector3.up), out hit, currentHeight))
            return false;
        if (Physics.Raycast(new Vector3(transform.position.x + cc.radius, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.up), out hit, currentHeight))
            return false;
        if (Physics.Raycast(new Vector3(transform.position.x - cc.radius, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.up), out hit, currentHeight))
            return false;

        return true;
    }
    #endregion

    #region Technical Methods

    void VariablesAssignment()
    {
        cc = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>().transform;

        PV = GetComponent<PhotonView>();
    }

    bool ErrorHandling()
    {
        if (cc == null)
        {
            ErrorHandler.Instance.ComponentIsMissing("Character Controller", gameObject);
            return false;
        }
        if (playerCam == null)
        {
            ErrorHandler.Instance.GameObjectIsMissing("Camera");
            return false;
        }
        return true;
    }

    #endregion
}
