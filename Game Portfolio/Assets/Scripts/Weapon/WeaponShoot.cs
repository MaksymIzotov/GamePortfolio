using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    #region Public Variables

    [Tooltip("Check this if gun uses auto fire mode")]
    public bool isAuto = false;
    [Tooltip("Time between shoots if auto")]
    public float timeBetweenShoots = 1f;
    [Tooltip("Amount of delay between shots if no auto")]
    public float delayBeforeShots = 1f;
    [Tooltip("Speed of the bullet")]
    public float bulletForceAmount = 1f;
    [Tooltip("Camera to be shot from")]
    public GameObject cam;


    [Tooltip("Bullet prefab to be shot")]
    public GameObject bulletPrefab;

    #endregion

    #region Private Variables

    private bool canShoot = true;

    #endregion

    #region Unity Methods

    private void Update()
    {
        HandleInput();
    }

    #endregion

    #region User Methods

    void HandleInput()
    {
        if (isAuto)
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
        GameObject bullet = Instantiate(bulletPrefab, cam.transform.position, cam.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(-transform.right * bulletForceAmount, ForceMode.Impulse);
    }

    IEnumerator ShootOnce()
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(delayBeforeShots);

        canShoot = true;
    }

    IEnumerator ShootAuto()
    {
        while (Input.GetKey(InputManager.Instance.Shoot))
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    #endregion

    #region Technical Methods



    #endregion
}
