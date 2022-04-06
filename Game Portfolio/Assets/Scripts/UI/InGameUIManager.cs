using UnityEngine;
using Photon.Pun;

public class InGameUIManager : MonoBehaviour
{
    public enum UISTATE
    {
        PLAY = 1,
        PAUSE = 2
    }

    public static InGameUIManager Instance;
    [HideInInspector] public UISTATE state;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = UISTATE.PLAY;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == UISTATE.PLAY)
            {
                state = UISTATE.PAUSE;
                MenuManager.Instance.OpenMenu("esc");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (state == UISTATE.PAUSE)
            {
                state = UISTATE.PLAY;
                MenuManager.Instance.OpenMenu("hud");
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ResumeGame()
    {
        state = UISTATE.PLAY;
        MenuManager.Instance.OpenMenu("hud");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LeaveServer()
    {
        Destroy(RoomManager.Instance.gameObject);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
