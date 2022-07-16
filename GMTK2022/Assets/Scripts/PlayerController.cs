using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    Rigidbody2D rb;
    [Space(2)]
    [Header("Horizontal Movement")]
    [SerializeField] float runSpeed;
    [SerializeField] float rollTime;
    [SerializeField] float rollSpeed;
    [SerializeField] float rollDelay;
    private float nextAvailableRollTime = 0;
    [Space(2)]
    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2;
    [Space(2)]
    [Header("Conditions")]
    [SerializeField] Vector2 jumpColliderBottomOffset;
    [SerializeField] float jumpColliderRadius;
    public bool facingRight; //the mouse is to the right of the player;
    private bool isTouchingGround = false;
    private bool isRolling = false;
    [Header("LayerMasks")]
    [SerializeField] LayerMask groundLayer;

    //Animation
    private Animator anim;
    #endregion 
    private void Start()
    {
        //Initializing Components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        ConditionsCheck();
        HorizontalMovement();
        RollMovement();
        VerticalMovement();
        CheckDirection();
    }
    void ConditionsCheck()
    {
        //Checks Conditions regarding a state a player is in relation to its actions and the environment around it.
        isTouchingGround = Physics2D.OverlapCircle((Vector2)transform.position + jumpColliderBottomOffset, jumpColliderRadius, groundLayer);
    }



    void CheckDirection()
    {
        Vector3 worldPosMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosMouse.z = 0;
        if(worldPosMouse.x > transform.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        if (facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    #region Horizontal Movement
    void HorizontalMovement()
    {
        if (isRolling)
        {
            return;
        }


        if (Input.GetKey(KeyCode.D))
        {
            //transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void RollMovement()
    {
        if (Input.GetKeyDown(KeyCode.S) && Time.time > nextAvailableRollTime && isTouchingGround)
        {
            nextAvailableRollTime = Time.time + rollDelay + rollTime;
            StartCoroutine(Roll(rollTime, rollSpeed, (int)transform.localScale.x));
        }
    }
    IEnumerator Roll(float rollTime, float rollSpeed, int direction)
    {
        isRolling = true;
        rb.velocity = new Vector2(rollSpeed * direction, rb.velocity.y);
        yield return new WaitForSeconds(rollTime);

        isRolling = false;
    }

    #endregion



    #region Vertical Movement
    void VerticalMovement() //Note to self: might remove the fancy hold to jump higher thing
    {
        if (isTouchingGround)
        {
            Jump();
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        if (rb.velocity.y < -0.1f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + jumpColliderBottomOffset, jumpColliderRadius);
    }
}

