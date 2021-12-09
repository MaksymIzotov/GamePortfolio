using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadBob : MonoBehaviour
{
    CharacterController cc;

    public Transform camera;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        InAirCameraMovement();
    }

    void InAirCameraMovement()
    {
        camera.rotation = new Quaternion(cc.velocity.y,0,0,0);
    }
}
