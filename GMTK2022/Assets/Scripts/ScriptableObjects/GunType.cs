using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Guns")]
public class GunType : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite cardArt;
    public Sprite armArt;
    public float bulletSpeed;
    public float fireRate; //delay between shots
    public float bulletSpread;
    public int maxAmmo;
    public float reloadTime; //how long it takes to reload
    public float shotgunShots; //how many bullets per shot
    public int damage;
    public int rarity; //from 0 to 3
    public Vector2 offSet;
}
