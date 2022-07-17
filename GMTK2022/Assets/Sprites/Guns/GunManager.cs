using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    public static GunManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //}
        //DontDestroyOnLoad(this);
    }
    public event EventHandler onChoseCard;
    public void CardChoose(GunType choosenGun)
    {
        currentGun = choosenGun;
        onChoseCard?.Invoke(this, EventArgs.Empty);
    }
}
