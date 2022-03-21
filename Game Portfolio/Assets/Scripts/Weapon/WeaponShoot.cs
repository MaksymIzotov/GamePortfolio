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

    private WeaponInfo info;
    private WeaponRecoil recoil;
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
        HandleInput();
    }

    #endregion

    #region User Methods

    void HandleInput()
    {
        if (info.isAuto)
        {
            if (Input.GetKeyDown(InputManager.Instance.Shoot))
                StartCoroutine(ShootAuto());
            else if(Input.GetKeyUp(InputManager.Instance.Shoot))
                StopAllCoroutines();
        }
        else
        {
            if (Input.GetKeyDown(InputManager.Instance.Shoot) && canShoot)
                StartCoroutine(ShootOnce());
        }
    }

    void Shoot()
    {
        float r = info.radius;

        GameObject bullet = Instantiate(bulletPrefab, cam.transform.position, cam.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce((transform.forward - new Vector3(Random.Range(-r,r), Random.Range(-r, r), Random.Range(-r, r))*0.5f) * info.bulletForceAmount, ForceMode.Impulse);

        recoil.AddRecoil(info.recoilX, info.recoilY, info.recoilZ);
    }

    IEnumerator ShootOnce()
    {
        canShoot = false;

        ShotgunCheck();
        yield return new WaitForSeconds(info.shootingDelay);

        canShoot = true;
    }

    IEnumerator ShootAuto()
    {
        while (Input.GetKey(InputManager.Instance.Shoot))
        {
            ShotgunCheck();
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

    #endregion

    #region Technical Methods

    bool AssignVariables()
    {
        cam = GameObject.Find("PlayerCamera");
        info = GetComponent<WeaponInfo>();
        recoil = GameObject.Find("CameraRecoil").GetComponent<WeaponRecoil>();

        if (cam == null)
            return false;

        return true;
    }

    #endregion
}
