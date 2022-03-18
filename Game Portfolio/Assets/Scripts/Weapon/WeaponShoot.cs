using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    #region Public Variables
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
    public float delayBeforeShots = 1f;
    [Tooltip("Speed of the bullet")]
    public float bulletForceAmount = 1f;
    [Tooltip("Camera to be shot from")]
    public GameObject cam;

    [Header("Bullet spreading")]
    [Tooltip("Radius of shooting spread")]
    public float radius = 1f;


    [Tooltip("Bullet prefab to be shot")]
    public GameObject bulletPrefab;

    #endregion

    #region Private Variables

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
        bullet.GetComponent<Rigidbody>().AddForce((transform.forward - new Vector3(Random.Range(-radius,radius), Random.Range(-radius, radius), Random.Range(-radius, radius))*0.5f) * bulletForceAmount, ForceMode.Impulse); ;
    }

    IEnumerator ShootOnce()
    {
        canShoot = false;

        ShotgunCheck();
        yield return new WaitForSeconds(delayBeforeShots);

        canShoot = true;
    }

    IEnumerator ShootAuto()
    {
        while (Input.GetKey(InputManager.Instance.Shoot))
        {
            ShotgunCheck();
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    void ShotgunCheck()
    {
        if (!isShotgun)
        {
            Shoot();
        }
        else
        {
            for (int i = 0; i < pelletsAmount; i++)
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

        if (cam == null)
            return false;

        return true;
    }

    #endregion
}
