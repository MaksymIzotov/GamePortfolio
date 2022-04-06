using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviour
{
    PhotonView pv;

    public GameObject[] items;
    public GameObject[] groundItems;
    public Transform itemParent;
    public float dropForce;

    private int currentItem;

    private GameObject itemInHands;
    private GameObject[] inventory;

    private void Start()
    {
        pv = GetComponent<PhotonView>();

        currentItem = 0;

        inventory = new GameObject[3];
    }
    private void Update()
    {
        if (!pv.IsMine) { return; }
        if (InGameUIManager.Instance.state == InGameUIManager.UISTATE.PAUSE) { return; }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0, false);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1, false);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeapon(2, false);

        if (Input.GetKeyDown(InputManager.Instance.Drop))
            DropItem();
    }

    private void ChangeWeapon(int index, bool isPicked)
    {
        if (currentItem == index && !isPicked) { return; }

        if (itemInHands != null)
            Destroy(itemInHands);

        currentItem = index;

        if (inventory[currentItem] == null) { UIUpdater.Instance.UpdateAmmoText(0); return; }

        itemInHands = Instantiate(inventory[currentItem], itemParent); //Pickup item

        UIUpdater.Instance.UpdateAmmoText(itemInHands.GetComponent<WeaponInfo>().maxAmmo);
    }

    public bool PickupItem(int index)
    {
        for (int i = 0; i < inventory.Length; i++) //Place it in inventory if free slot exist
        {
            if (inventory[i] == null)
            {
                inventory[i] = items[index];

                if (currentItem == i)
                    ChangeWeapon(i, true);

                return true;
            }
        }

        Debug.Log("No space in inventory");
        return false;
    }

    public void DropOnDeath()
    {
        for (int i = 0; i < inventory.Length; i++) 
        {
            if(inventory[i] != null)
            {
                int itemName = Random.Range(0, 1000000);
                Vector3 torque = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                GetComponent<PhotonView>().RPC("RPC_DropItem", RpcTarget.All, inventory[i].GetComponent<ItemsInfo>().index, transform.position, transform.rotation, torque, itemName);
            }
        }
    }

    private void DropItem()
    {
        if (itemInHands == null) { return; }

        int index = itemInHands.GetComponent<ItemsInfo>().index;

        Destroy(itemInHands);
        inventory[currentItem] = null;

        Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        int itemName = Random.Range(0, 1000000);

        GetComponent<PhotonView>().RPC("RPC_DropItem", RpcTarget.All, index, transform.position, transform.rotation, torque, itemName);
        
    }

    [PunRPC]
    void RPC_DropItem(int index, Vector3 pos, Quaternion rot, Vector3 torque, int itemName)
    {
        GameObject go = Instantiate(groundItems[index], pos, rot);
        go.name = itemName.ToString("0000000");

        if (torque != Vector3.zero)
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * dropForce, ForceMode.Impulse);

        go.GetComponent<Rigidbody>().AddTorque(torque * dropForce / 2, ForceMode.Impulse);
    }
}
