using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [Header("Shooting variables")]
    [Tooltip("Check this if gun uses auto fire mode")]
    public bool isAuto = false;
    [Tooltip("Check this if gun is a shotgun")]
    public bool isShotgun = false;
    [Tooltip("Amonunt of pellets in one shot")]
    public int pelletsAmount = 1;
    [Tooltip("Time between shoots if auto")]
    public float timeBetweenShoots = 1f;
    [Tooltip("Amount of delay between shots if no auto")]
    public float shootingDelay = 1f;
    [Tooltip("Speed of the bullet")]
    public float bulletForceAmount = 1f;

    [Header("Bullet spreading")]
    [Tooltip("Radius of shooting spread")]
    public float radius = 1f;

    [Header("Recoil")]
    [Tooltip("Recoil amount by X Axis")]
    public float recoilX;
    [Tooltip("Recoil amount by Y Axis")]
    public float recoilY;
    [Tooltip("Recoil amount by Z Axis")]
    public float recoilZ;

}
