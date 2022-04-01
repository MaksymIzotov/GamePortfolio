using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public float sensitivity;

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
    [HideInInspector] public KeyCode Drop;

    private bool isChanging = false;
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
        Drop = KeyCode.G;
    }


    public void SetKeyCode(GameObject button)
    {
        switch (button.name) {
            case "Jump":
                isChanging = true;
                //Update UI
                break;
        }

    }

    void OnGUI()
    {
        if (!isChanging) { return; }

        Event e = Event.current;
        if (e.isKey)
        {
            

        }
    }

    private void DetectKey(ref KeyCode keyCode, KeyCode changeTo)
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log(e.keyCode);
            Jump = e.keyCode;
        }
    }
}
