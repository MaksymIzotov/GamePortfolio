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

    #endregion

    #region Private Variables

    Vector3 offset;

    #endregion

    #region Unity Methods
    void Start()
    {
        VariablesAssignment();
    }


    void Update()
    {
        Follow();
    }

    #endregion

    #region Created Methods

    void Follow()
    {
        //TODO: gun sway
    }

    #endregion

    #region Technical Methods

    void VariablesAssignment()
    { 
        if (cam == null)          
            ErrorHandler.Instance.GameObjectIsMissing("Camera");
    }

    #endregion
}
