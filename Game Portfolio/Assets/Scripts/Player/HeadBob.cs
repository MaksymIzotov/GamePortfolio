using UnityEngine;


public class HeadBob : MonoBehaviour
{
    #region Public Variables

    [Tooltip("Camera of the player")]
    public Transform playerCamera;

    [Tooltip("Amount of camera movement while running")]
    public float bobbingRunAmount = 0.05f;

    [Tooltip("Bobbing speed multiplier")]
    public float bobbingMultiplier = 1;

    #endregion

    #region Private Variables

    float defaultPosY = 0;
    float timer = 0;

    float walkingSpeed;

    #endregion

    #region Unity Methods

    private void Start()
    {
        VariablesAssignment();
    }

    void Update()
    {
        RunHeadBob(GetComponent<PlayerController>().speed);
    }

    #endregion

    #region Created Methods

    void RunHeadBob(float speed)
    {
        if (GetComponent<PlayerController>().isWalking)
        {
            //Player is moving
            timer += Time.deltaTime * (speed - walkingSpeed) * bobbingMultiplier;
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingRunAmount, playerCamera.transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, Mathf.Lerp(playerCamera.transform.localPosition.y, defaultPosY, 0.0001f), playerCamera.transform.localPosition.z);
        }
    }

    #endregion

    #region Technical Methods

    void VariablesAssignment()
    {
        walkingSpeed = GetComponent<PlayerController>().walkSpeed;
        defaultPosY = playerCamera.transform.localPosition.y;
    }

    #endregion
}
