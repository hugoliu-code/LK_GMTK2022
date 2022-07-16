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
    public bool isRolling = false;
    [Space(2)]
    [Header("LayerMasks")]
    [SerializeField] LayerMask groundLayer;

    //Animation
    [Space(2)]
    [Header("Animation and Sprites")]
    [SerializeField] Animator anim;
    private State state;
    [Space(2)]
    [Header("Controllers")]
    private GameController gc;
    enum State
    {
        Run,
        RunBackwards,
        Idle,
        Jump,
        Fall,
        Roll
    }
    #endregion 
    private void Start()
    {
        //Initializing Components
        rb = GetComponent<Rigidbody2D>();
        gc = FindObjectOfType<GameController>();
    }
    
    private void Update()
    {
        ConditionsCheck();
        HorizontalMovement();
        RollMovement();
        VerticalMovement();
        CheckDirection();
        AnimationController();
    }
    public void TakeDamage()
    {
        //play some kind of animation to show it

        //Remove Health
        gc.health -= 1;
    }
    void ConditionsCheck()
    {
        //Checks Conditions regarding a state a player is in relation to its actions and the environment around it.
        isTouchingGround = Physics2D.OverlapCircle((Vector2)transform.position + jumpColliderBottomOffset, jumpColliderRadius, groundLayer);
    }

    void AnimationController()
    {
        if (isRolling)
        {
            state = State.Roll;
        }
        else if(rb.velocity.y > 0.1f)
        {
            state = State.Jump;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = State.Fall;
        }
        else if((rb.velocity.x > 0.1f && facingRight) || (rb.velocity.x < -0.1f && !facingRight))
        {
            state = State.Run;
        } 
        else if((rb.velocity.x < -0.1f && facingRight) || (rb.velocity.x > 0.1f && !facingRight))
        {
            state = State.RunBackwards;
        }
        else
        {
            state = State.Idle;
        }
        anim.SetInteger("State", (int)state);
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
            int direction = (int)transform.localScale.x;
            if(rb.velocity.x > 0.1f)
            {
                direction = 1;
            }
            else if(rb.velocity.x<-0.1f)
            {
                direction = -1;
            }
            StartCoroutine(Roll(rollTime, rollSpeed, direction));
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

