using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFollowScript : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject gunTipIndicator;
    private GunManager gm;
    private SpriteRenderer sr;
    private void Start()
    {
        gm = FindObjectOfType<GunManager>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = gm.currentGun.armArt;
        gunTipIndicator.transform.localPosition = gm.currentGun.offSet;
    }
    private void Update()
    {
        if (player.isRolling)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosMouse.z = 0;
        float angleOfRotation = Mathf.Atan2(worldPosMouse.y - transform.position.y, worldPosMouse.x - transform.position.x) * Mathf.Rad2Deg;
        if (player.facingRight)
            transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
        else
            transform.rotation = Quaternion.Euler(0, 0, angleOfRotation + 180);
    }
}
