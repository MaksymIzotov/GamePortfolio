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

    private ErrorHandler errorHandler;
    private Rigidbody rb;

    private Vector3 playerVelocity;
    private bool isGrounded;

    private float currentHeight;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        errorHandler = GameObject.FindGameObjectWithTag("GameMechanics").GetComponent<ErrorHandler>();

        if(errorHandler == null)
            Debug.LogError("ERROR HANDLER IS MISSING");
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null && errorHandler)
            errorHandler.ComponentIsMissing("Rigidbody", gameObject);

        //currentHeight = cc.height;
    }

    private void Update()
    {
        MovePlayer();
        //Crouch();
    }

    #endregion

    #region Created Methods

    void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
        }

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //cc.Move(movement * velocity * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        //cc.Move(playerVelocity * Time.deltaTime);
        //isGrounded = cc.isGrounded;
    }

    //void Crouch()
    //{
    //    if (Input.GetKey(KeyCode.LeftControl))
    //        cc.height = Mathf.Lerp(cc.height, crouchHeight, crouchTime);
    //    else
    //        cc.height = Mathf.Lerp(cc.height, currentHeight, crouchTime);
    //}

    #endregion
}
