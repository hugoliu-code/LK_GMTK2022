using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GunController : MonoBehaviour
{
    public event EventHandler onReload;
    public event EventHandler onReloadFinish;
    #region Variables
    [Header("Gun Stats")]
  
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate; //delay between shots
    [SerializeField] float bulletSpread;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] float reloadTime; //how long it takes to reload
    private float finishReloadTime = 0;
    [SerializeField] float shotgunShots = 1; //how many bullets per shot
    [SerializeField] int damage;
    public int currentAmmo;
    public bool isReloading = false;
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
        gm = FindObjectOfType<GunManager>();
        UpdateGun();
        currentAmmo = maxAmmo;
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
            if(currentAmmo <= 0 && !isReloading && currentAmmo != maxAmmo)
            {
                onReload?.Invoke(this, EventArgs.Empty);
                isReloading = true;
                finishReloadTime = Time.time + reloadTime;
                FMODUnity.RuntimeManager.PlayOneShot(gm.currentGun.audioPathReload, GetComponent<Transform>().position);
                Invoke("Reload", reloadTime);
            }
            if (Time.time < nextAvailableFireTime || currentAmmo <= 0 || player.isRolling || isReloading)
            {
                return;
            }
            for (int a = 0; a < shotgunShots; a++)
            {
                if (a == 0)
                {
                    currentAmmo -= 1;
                    FMODUnity.RuntimeManager.PlayOneShot(gm.currentGun.audioPathShoot, GetComponent<Transform>().position);
                    Shoot();
                }
                else
                {
                    Shoot();
                }
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
        float spread = UnityEngine.Random.Range(-bulletSpread / 2, bulletSpread / 2);
        Vector3 worldPosMouseWithSpread = worldPosMouse - gunTipIndicator.position; //the relative vector from P2 to P1.
        worldPosMouseWithSpread = Quaternion.Euler(0, 0, spread) * worldPosMouseWithSpread; //rotatate
        worldPosMouseWithSpread = gunTipIndicator.position + worldPosMouseWithSpread; //bring back to world space


        //Update ammo
        
        //Generating the Bullet
        GameObject bullet = Instantiate(normalBullet, gunTipIndicator.position, Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<Rigidbody2D>().velocity = (worldPosMouseWithSpread - gunTipIndicator.position).normalized * bulletSpeed;
        bullet.GetComponent<BulletScript>().damage = damage;

        //Screenshake
        //gm.screenShake.SmallShake();

        //SOUND

        //New Last Shot Time
        nextAvailableFireTime = Time.time + fireRate;
    }
    void ReloadController()
    {
        if (Input.GetKeyDown(KeyCode.R) && finishReloadTime < Time.time)
        {
            onReload?.Invoke(this, EventArgs.Empty);
            isReloading = true;
            finishReloadTime = Time.time + reloadTime;

            FMODUnity.RuntimeManager.PlayOneShot(gm.currentGun.audioPathReload, GetComponent<Transform>().position);
            Invoke("Reload", reloadTime);
        }
    }
    void Reload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        onReloadFinish?.Invoke(this,EventArgs.Empty);
    }

}

