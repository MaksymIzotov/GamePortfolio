using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] items;
    public Transform itemParent;

    private int currentItem;

    private GameObject itemInHands;
    private GameObject[] inventory;

    private void Start()
    {
        currentItem = 0;

        inventory = new GameObject[10];
    }
    private void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0, false);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1, false);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeapon(2, false);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeWeapon(3, false);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeWeapon(4, false);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeWeapon(5, false);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            ChangeWeapon(6, false);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            ChangeWeapon(7, false);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            ChangeWeapon(8, false);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ChangeWeapon(9, false);
    }

    private void ChangeWeapon(int index, bool isPicked)
    {
        if (currentItem == index && !isPicked) { return; }

        if (itemInHands != null)
            GameobjectDestroyer.Instance.DestroyGO(itemInHands);

        currentItem = index;

        if (inventory[currentItem] == null) { return; }

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
}
