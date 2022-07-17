using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject gunTipIndicator;
    private ShootEnemyController enemyController;
    [SerializeField] float minimumDetectDistance;
    private bool seesPlayer = false;
    [SerializeField] LayerMask detectables;
    [SerializeField] float shotDelay;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject mainBody;
    private float nextShot;
    private GunManager gm;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        
    }

    private void Update()
    {
        Vector3 offsetTransform = transform.position + new Vector3(0, 1, 0);
        RaycastHit2D collisionCheck = Physics2D.Raycast(offsetTransform, player.transform.position - offsetTransform, Vector2.Distance(offsetTransform, player.transform.position), detectables);
        
        if (collisionCheck && collisionCheck.collider.gameObject.layer == LayerMask.NameToLayer("PlayerHitbox"))
        {
            if(Vector2.Distance(offsetTransform, collisionCheck.point) <= minimumDetectDistance)
            {
                if(collisionCheck.point.x > transform.position.x)
                {
                    mainBody.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    mainBody.transform.localScale = new Vector3(-1, 1, 1);
                }
                seesPlayer = true;
            }
            else
            {
                seesPlayer = false;
            }
            if (Vector2.Distance(offsetTransform, collisionCheck.point) <= minimumDetectDistance && nextShot <Time.time)
            {
                nextShot = Time.time + shotDelay;
                Debug.Log("bruh");
            }
            seesPlayer = false;
        }
        float angleOfRotation = 0;
        if (seesPlayer)
            angleOfRotation = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

        if (collisionCheck.point.x > transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
        else
            transform.rotation = Quaternion.Euler(0, 0, angleOfRotation + 180);
    }
}
