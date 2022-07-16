using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject playerHitbox;
    private bool isAttacking;
    [SerializeField] float minimumDetectDistance;
    [SerializeField] float runSpeed = 5f;
    private bool isTouchingGround;
    private bool notSeenPlayerYet = true;
    private Vector2 lastSeenPosition; //the last known position of the player
    [SerializeField] LayerMask detectables; //walls and player
    [SerializeField] Vector2 jumpColliderBottomOffset;
    [SerializeField] float jumpColliderRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float attackForce;
    [SerializeField] float chargeTime;
    [SerializeField] float jumpDelay;
    [SerializeField] Vector2 attackDirection = new Vector2(1,1);
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHitbox = FindObjectOfType<PlayerTakeDamageScript>().gameObject;
    }
    private void Update()
    {
        CheckGround();
        DetectPlayer();
        MoveToPlayer();

    }
    private void CheckGround()
    {
        isTouchingGround = Physics2D.OverlapCircle((Vector2)transform.position + jumpColliderBottomOffset, jumpColliderRadius, groundLayer);
    }
    private void DetectPlayer()
    {
        Vector3 offsetTransform = transform.position + new Vector3(0, 1, 0);
        RaycastHit2D collisionCheck = Physics2D.Raycast(offsetTransform, playerHitbox.transform.position - offsetTransform, Vector2.Distance(offsetTransform, playerHitbox.transform.position), detectables);

        if(collisionCheck && collisionCheck.collider.gameObject.layer == LayerMask.NameToLayer("PlayerHitbox"))
        {
            if(Vector2.Distance(offsetTransform, collisionCheck.point) <= minimumDetectDistance && !isAttacking && isTouchingGround)
            {
                StartCoroutine(Attack(collisionCheck.point));
            }
            notSeenPlayerYet = false;
            lastSeenPosition = collisionCheck.point;
        }
    }
    private void MoveToPlayer()
    {
        if (notSeenPlayerYet || isAttacking)
        {
            return;
        }
        if(transform.position.x > lastSeenPosition.x+1f)
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
        }
        else if (transform.position.x < lastSeenPosition.x - 1f)
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
    }

    IEnumerator Attack(Vector3 target)
    {
        isAttacking = true;
        rb.velocity = new Vector2(0, 0);

        //Wait a second to charge
        yield return new WaitForSeconds(chargeTime);

        Vector2 forceToAdd = attackDirection.normalized * attackForce;
        if(target.x < transform.position.x)
        {
            forceToAdd.x = forceToAdd.x * -1;
        }
        rb.AddForce(forceToAdd);
        yield return new WaitForSeconds(jumpDelay);
        while (!isTouchingGround)
        {
            yield return null;
        }

        isAttacking = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + jumpColliderBottomOffset, jumpColliderRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, minimumDetectDistance);
    }
}
