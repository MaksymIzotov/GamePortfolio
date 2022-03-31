using UnityEngine;
using Photon.Pun;

public class GameobjectDestroyer : MonoBehaviourPun
{
    PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void DestroyGO(GameObject go)
    {
        pv.RPC("DestroyGO_RPC", RpcTarget.All, go.name);
    }

    [PunRPC]
    void DestroyGO_RPC(string go)
    {
        Destroy(GameObject.Find(go));
    }
}
