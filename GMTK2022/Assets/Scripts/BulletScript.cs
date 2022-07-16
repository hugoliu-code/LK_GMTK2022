using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask collidables; //objects, ground, enemies

    private Vector3 previousPosition; //position of the object the frame before
    public int damage = 1;
    private void Start()
    {
        previousPosition = transform.position;
    }
    private void Update()
    {
        RaycastHit2D collisionCheck = Physics2D.Raycast(previousPosition, transform.position - previousPosition, Vector2.Distance(previousPosition, transform.position), collidables);
       
        if (collisionCheck)
        {
            if(collisionCheck.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHitbox")){
                collisionCheck.collider.gameObject.GetComponent<EnemyDamageHandler>().TakeDamage(damage);
            }
            HandleCollision(collisionCheck);
        }
        previousPosition = transform.position;
    }

    void HandleCollision(RaycastHit2D collision)
    {
        transform.position = collision.point;
        Invoke("CustomDestroy", 0.01f);
    }
    void CustomDestroy()
    {
        Destroy(this.gameObject);
    }
}
