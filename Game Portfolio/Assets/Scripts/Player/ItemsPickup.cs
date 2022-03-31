using UnityEngine;
using Photon.Pun;

public class ItemsPickup : MonoBehaviourPun
{
    public Transform cam;
    public float distance;

    private RaycastHit hit;


    void Update()
    {
        CheckItems();
    }

    private void CheckItems()
    {
        if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, distance))
        {
            Debug.DrawRay(cam.position, cam.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if(hit.transform.tag == "Pickup")
            {
                if (!UIUpdater.Instance.pickup.activeSelf)
                    UIUpdater.Instance.UpdatePickupGUI(true);

                if (Input.GetKeyDown(InputManager.Instance.Pickup))
                    Pickup(hit.transform.gameObject);
            }
            else
            {
                if(UIUpdater.Instance.pickup.activeSelf)
                    UIUpdater.Instance.UpdatePickupGUI(false);
            }
        }
        else
        {
            if (UIUpdater.Instance.pickup.activeSelf)
                UIUpdater.Instance.UpdatePickupGUI(false);
        }
    }

    private void Pickup(GameObject go)
    {
        if (GetComponent<WeaponController>().PickupItem(go.GetComponent<ItemsInfo>().index))
            GetComponent<GameobjectDestroyer>().DestroyGO(go);
    }
}
