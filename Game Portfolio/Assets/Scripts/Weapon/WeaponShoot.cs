using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponInfo))]
public class WeaponShoot : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Camera to be shot from")]
    public GameObject cam;
    [Tooltip("Bullet prefab to be shot")]
    public GameObject bulletPrefab;

    #endregion

    #region Private Variables

    private WeaponRecoil recoil;
    private WeaponAnimations weaponAnimation;
    private WeaponStateController stateController;
    private WeaponInfo info;
    private bool canShoot = true;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (!AssignVariables())
            Debug.LogError("SOMETHING WENT WRONG DURING START METHOD");
    }

    private void Update()
    {
        if (info.weaponState == WeaponInfo.State.PICKUP) { return; }

        HandleInput();
    }

    #endregion

    #region User Methods

    void HandleInput()
    {
        if (!canShoot) { return; }

        if (Input.GetKeyDown(InputManager.Instance.Reload) && info.weaponState != WeaponInfo.State.RELOAD)
            if (info.currentAmmo < info.maxAmmo)
                Reload();

        if (info.isAuto)
        {
            if (Input.GetKeyDown(InputManager.Instance.Shoot))
                StartCoroutine(ShootAuto());
            else if(Input.GetKeyUp(InputManager.Instance.Shoot))
                StopAllCoroutines();
        }
        else
        {
            if (Input.GetKeyDown(InputManager.Instance.Shoot))
                StartCoroutine(ShootOnce());
        }
    }

    void Shoot()
    {
        float r = info.radius;

        //Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, cam.transform.position, cam.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce((transform.forward - new Vector3(Random.Range(-r,r), Random.Range(-r, r), Random.Range(-r, r))*0.5f) * info.bulletForceAmount, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().damage = info.baseDamage;

        //Add recoil
        recoil.AddRecoil(info.recoilX, info.recoilY, info.recoilZ);

        //Play animation
        weaponAnimation?.Shoot();

    }

    IEnumerator ShootOnce()
    {
        canShoot = false;

        ShotgunCheck();
        info.currentAmmo -= 1;

        UIUpdater.Instance.UpdateAmmoText(info.currentAmmo);

        yield return new WaitForSeconds(info.shootingDelay);

        canShoot = true;

        if (info.currentAmmo <= 0)
            Reload();
    }

    IEnumerator ShootAuto()
    {
        while (Input.GetKey(InputManager.Instance.Shoot))
        {
            ShotgunCheck();
            info.currentAmmo -= 1;

            UIUpdater.Instance.UpdateAmmoText(info.currentAmmo);

            if (info.currentAmmo <= 0)
                Reload();

            yield return new WaitForSeconds(info.timeBetweenShoots);
        }
    }

    void ShotgunCheck()
    {
        if (!info.isShotgun)
        {
            Shoot();  
        }
        else
        {
            for (int i = 0; i < info.pelletsAmount; i++)
            {
                Shoot();
            }
        }
    }

    private void Reload()
    {
        StopAllCoroutines();
        stateController.SetState(WeaponInfo.State.RELOAD);
        StartCoroutine(ReloadTimer());
    }

    IEnumerator ReloadTimer()
    {
        canShoot = false;
        weaponAnimation.Reload();

        yield return new WaitForSeconds(info.reloadTime);
        stateController.SetState(WeaponInfo.State.IDLE);
        info.currentAmmo = info.maxAmmo;

        UIUpdater.Instance.UpdateAmmoText(info.currentAmmo);

        canShoot = true;
    }

    #endregion

    #region Technical Methods

    bool AssignVariables()
    {

        cam = GameObject.Find("PlayerCamera");
        recoil = GameObject.Find("CameraRecoil").GetComponent<WeaponRecoil>();
        weaponAnimation = GetComponent<WeaponAnimations>();
        stateController = GetComponent<WeaponStateController>();
        info = GetComponent<WeaponInfo>();

        if (cam == null)
            return false;

        info.currentAmmo = info.maxAmmo;

        return true;
    }

    #endregion
}
