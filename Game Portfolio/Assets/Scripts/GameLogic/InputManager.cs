using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector] public KeyCode Forward;
    [HideInInspector] public KeyCode Backward;
    [HideInInspector] public KeyCode Left;
    [HideInInspector] public KeyCode Right;

    [HideInInspector] public KeyCode Jump;
    [HideInInspector] public KeyCode Crouch;
    [HideInInspector] public KeyCode Run;

    [HideInInspector] public KeyCode Shoot;
    [HideInInspector] public KeyCode Aim;
    [HideInInspector] public KeyCode Reload;

    [HideInInspector] public KeyCode Pickup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AssignDefaults();
    }

    public void AssignDefaults()
    {
        Forward = KeyCode.W;
        Backward = KeyCode.S;
        Left = KeyCode.A;
        Right = KeyCode.D;

        Jump = KeyCode.Space;
        Crouch = KeyCode.LeftControl;
        Run = KeyCode.LeftShift;

        Shoot = KeyCode.Mouse0;
        Aim = KeyCode.Mouse1;
        Reload = KeyCode.R;

        Pickup = KeyCode.E;
    }
}
