using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables

    [Tooltip("How fast player will move")]
    public float velocity = 5f;
    [Tooltip("Gravity forces that applies to the player")]
    public float gravity = 19.6f;
    [Tooltip("Amount of force will apply to the player when jumping")]
    public float jumpForce = 5f;
    [Tooltip("Height of ducking")]
    public float crouchHeight = 0.5f;
    [Tooltip("Time to duck")]
    public float crouchTime = 1f;

    #endregion

    #region Private Variables
    private CharacterController cc;

    private Vector3 playerVelocity;
    private bool isGrounded;

    private float currentHeight;

    private float moveX;
    private float moveY;
    private bool isJumping;

    private Transform playerCam;

    #endregion

    #region Unity Methods

    void Start()
    {
        VariablesAssignment();

        GetComponent<PlayerController>().enabled = ErrorHandling();

        currentHeight = cc.height;
    }

    private void FixedUpdate()
    {
        //TODO: try to move all physics here as soon as you solve input problem
        MovePlayer();
    }

    private void Update()
    {
        InputManager();
        Crouch();
        Jump();
    }

    #endregion

    #region Created Methods

    void MovePlayer()
    {


        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 movement = Quaternion.Euler(0, playerCam.transform.eulerAngles.y, 0) * new Vector3(moveX, 0, moveY);
        movement = Vector3.ClampMagnitude(movement, 1);

        cc.Move(movement * velocity * cc.height * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
        isGrounded = cc.isGrounded;
    }

    void Jump()
    {
        if (isJumping && isGrounded && !Input.GetButton("Crouch"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
        }
    }

    void Crouch()
    {
        if (Input.GetButton("Crouch") && isGrounded)
            cc.height = Mathf.Lerp(cc.height, crouchHeight, crouchTime);
        else if(CheckHeight())
            cc.height = Mathf.Lerp(cc.height, currentHeight, crouchTime);
    }

    void InputManager()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        isJumping = Input.GetButtonDown("Jump");
    }

    bool CheckHeight()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, currentHeight))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        return true;
    }
    #endregion

    #region Technical Methods

    void VariablesAssignment()
    {
        cc = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>().transform;
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
