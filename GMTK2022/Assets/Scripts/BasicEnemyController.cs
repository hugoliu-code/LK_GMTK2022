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
    public int health = 150;
    private bool isCharging;
    private State state;
    [SerializeField] Animator anim;
    enum State
    {
        Run,
        Attack,
        Idle,
        Charge
    }
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
        AnimationController();
    }
    private void AnimationController()
    {
        if (isCharging)
        {
            state = State.Charge;
        }
        else if (isAttacking)
        {
            state = State.Attack;
        }
        else if(Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            state = State.Run;
        }
        else
        {
            state = State.Idle;
        }
        anim.SetInteger("State", (int)state);
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
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x < lastSeenPosition.x - 1f)
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            notSeenPlayerYet = true;
        }
    }

    IEnumerator Attack(Vector3 target)
    {
        isAttacking = true;
        Vector2 forceToAdd = attackDirection.normalized * attackForce;
        transform.localScale = new Vector2(1, 1);
        if (target.x < transform.position.x)
        {
            forceToAdd.x = forceToAdd.x * -1;
            transform.localScale = new Vector2(-1, 1);
        }

        rb.velocity = new Vector2(0, 0);

        //Wait a second to charge
        isCharging = true;
        yield return new WaitForSeconds(chargeTime);
        isCharging = false;

        rb.AddForce(forceToAdd);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Poker Chip/Attack", GetComponent<Transform>().position);
        yield return new WaitForSeconds(jumpDelay);
        while (!isTouchingGround)
        {
            yield return null;
        }
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }





    public void TakeDamage(int damage)
    {
        health-= damage;
        if(health <= 0)
        {
            //do an animation of some kind
            //Maybe flash the sprite and delete it
            FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy/Poker Chip/EDeath", GetComponent<Transform>().position);
            Destroy(this.gameObject);
        }
        else
        {
            //do something that shows damage?
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + jumpColliderBottomOffset, jumpColliderRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, minimumDetectDistance);
    }
}
