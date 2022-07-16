using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFollowScript : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private void Update()
    {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosMouse.z = 0;
        float angleOfRotation = Mathf.Atan2(worldPosMouse.y - transform.position.y, worldPosMouse.x - transform.position.x) * Mathf.Rad2Deg;
        if (player.facingRight)
            transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
        else
            transform.rotation = Quaternion.Euler(0, 0, angleOfRotation + 180);
    }
}
