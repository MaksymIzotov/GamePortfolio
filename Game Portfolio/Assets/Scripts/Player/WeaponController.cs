using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] weapons;
    public Transform weaponParent;

    private int currentWeapon;

    private GameObject weapon;

    private void Start()
    {
        currentWeapon = 0;
    }
    private void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            ChangeWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            ChangeWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            ChangeWeapon();
        }
    }

    private void ChangeWeapon()
    {
        if (weapon != null)
            Destroy(weapon);

        weapon = Instantiate(weapons[currentWeapon], weaponParent);
    }
}
