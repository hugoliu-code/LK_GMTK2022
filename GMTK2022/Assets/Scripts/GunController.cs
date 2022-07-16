using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    #region Variables
    [Header("Gun Stats")]
  
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate; //delay between shots
    [SerializeField] float bulletSpread;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] float reloadTime; //how long it takes to reload
    [SerializeField] float shotgunShots = 1; //how many bullets per shot
    [SerializeField] int damage;
    private int currentAmmo;
    private float nextAvailableReloadTime = 0;
    private float nextAvailableFireTime = 0;
    [Space(2)]
    [Header("Object References")]
    [SerializeField] GameObject normalBullet;
    [SerializeField] Transform gunTipIndicator;
    [SerializeField] PlayerController player;
    private GunManager gm;
    #endregion
    private void Start()
    {
        currentAmmo = maxAmmo;
        gm = FindObjectOfType<GunManager>();
        UpdateGun();
    }
    private void UpdateGun()
    {
        GunType current= gm.currentGun;
        bulletSpeed = current.bulletSpeed;
        fireRate = current.fireRate;
        bulletSpread = current.bulletSpread;
        maxAmmo = current.maxAmmo;
        reloadTime = current.reloadTime;
        shotgunShots = current.shotgunShots;
        damage = current.damage;
    }
    private void Update()
    {
        ShootingController();
        ReloadController();
    }
    void ShootingController()
    {
        /* When the Mouse is clicked (TO BE CHANGED)
         * Get the Mouse Position and rotate it with a random spread
         * Then Draw a raycast to simulate shooting
         * Then Call coroutine to draw the tracer
         */
        if (Input.GetMouseButton(0))
        {
            if (Time.time < nextAvailableFireTime || currentAmmo <= 0 || player.isRolling)
            {
                return;
            }
            for (int a = 0; a < shotgunShots; a++)
            {
                if (a == 0)
                    Shoot();
                else
                    Invoke("Shoot", 0.1f);
            }
        }
    }
    void Shoot()
    {
        //IF not enough time has passed, return


        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosMouse.z = 0;

        //Gunshot Sound
        //FMODUnity.RuntimeManager.PlayOneShot("event:/Characters/Player/Pistol", GetComponent<Transform>().position);

        //Creating New endpoint with spread
        float spread = Random.Range(-bulletSpread / 2, bulletSpread / 2);
        Vector3 worldPosMouseWithSpread = worldPosMouse - gunTipIndicator.position; //the relative vector from P2 to P1.
        worldPosMouseWithSpread = Quaternion.Euler(0, 0, spread) * worldPosMouseWithSpread; //rotatate
        worldPosMouseWithSpread = gunTipIndicator.position + worldPosMouseWithSpread; //bring back to world space


        //Update ammo
        currentAmmo -= 1;
        //Generating the Bullet
        GameObject bullet = Instantiate(normalBullet, gunTipIndicator.position, Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<Rigidbody2D>().velocity = (worldPosMouseWithSpread - gunTipIndicator.position).normalized * bulletSpeed;
        bullet.GetComponent<BulletScript>().damage = damage;

        //Screenshake
        //gm.screenShake.SmallShake();

        //New Last Shot Time
        nextAvailableFireTime = Time.time + fireRate;
    }
    void ReloadController()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = maxAmmo;
        }
    }

}

