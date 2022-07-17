using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //This script will persist through scenes, and keep track of the current gun being used/selected
    //at each level, the player will pull from the "current gun" scriptable object stored here to update their own gunController
    //at each selection level, the randomly generated cards will pull from this Gunmanager, that holds lists of all rarities
    public GunType currentGun;
    public GunType[] commonGuns;
    public GunType[] uncommonGuns;
    public GunType[] rareGuns;
    public GunType[] epicGuns;
    public GunType[] legendaryGuns;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GunManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
