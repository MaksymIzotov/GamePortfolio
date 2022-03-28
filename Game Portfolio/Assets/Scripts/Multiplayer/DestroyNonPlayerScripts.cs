using Photon.Pun;
using UnityEngine;

public class DestroyNonPlayerScripts : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    public GameObject GFX;
    public GameObject Camera;
    public HeadBob hb;
    public PlayerMouseLook mouseLook;
    public PlayerController playerController;
    public WeaponController weaponController;
    public ItemsPickup itemsPickup;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        if (!PV.IsMine)
        {
            GFX.SetActive(true);

            Destroy(hb);
            Destroy(mouseLook);
            Destroy(playerController);
            Destroy(weaponController);
            Destroy(itemsPickup);
            Destroy(Camera);
        }
    }
}
