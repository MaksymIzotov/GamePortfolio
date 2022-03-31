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
            gameObject.tag = "Enemy";
            gameObject.layer = LayerMask.NameToLayer("Enemy");

            GFX.SetActive(true);

            Destroy(hb);
            Destroy(mouseLook);
            Destroy(playerController);
            Destroy(itemsPickup);
            Destroy(Camera);
        }
    }
}
