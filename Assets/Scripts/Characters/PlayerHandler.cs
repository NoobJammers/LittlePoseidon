using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    #region Variables
    [Header("Player Modifications")]
    [SerializeField] float playerSpeed;
    [SerializeField] float playerJumpForce;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayerMask; //Ground layer, used for detection
    [SerializeField] Transform groundCheck; //Position of circle collider created to check ground detection

    float groundedRadius = 0.2f; //Radius of circle collider for ground detection
    Rigidbody2D rigidBody;
    bool jumpCheckEnabled = false; //Variable to check ground detection, delay added to prevent checking as soon as user clicks jump button



    public StateMachine stateMachine { get; private set; }
    public StandingState standing { get; private set; }
    public JumpingState jumping { get; private set; }
    public CrouchingState crouching { get; private set; }


    #endregion


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        standing = new StandingState(this, stateMachine);
        jumping = new JumpingState(this, stateMachine);
        crouching = new CrouchingState(this, stateMachine);
        stateMachine.Initialize(standing);
    }

    void Update()
    {
        stateMachine.currentState.HandleInput();
        stateMachine.currentState.LogicUpdate();

    }


    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();

    }




    public void MovePlayer(float horizontal_input)
    {

        rigidBody.velocity = transform.right * horizontal_input * playerSpeed;
        if (horizontal_input == 0f)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            if (horizontal_input < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);

            }
            animator.SetBool("isRunning", true);

        }
    }



    public void PlayerJump()
    {
        //transform.Translate(Vector3.up * (playerHandler.CollisionOverlapRadius + 0.1f));
        rigidBody.AddForce(Vector3.up * playerJumpForce, ForceMode2D.Impulse); 
        animator.SetTrigger("jump");
        StartCoroutine(SetJumpCheckEnabled());
    }



    public void ResetMoveParams()
    {
        rigidBody.velocity = Vector3.zero;
        //rigidBody.angularVelocity = Vector3.zero;

    }




    public bool CheckCollisionOverlap()
    {
        if (jumpCheckEnabled)
        {
            if(Physics2D.OverlapCircle(groundCheck.position, groundedRadius, groundLayerMask))
            {
                jumpCheckEnabled = false;
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }




    public void SetBoolAnimations(string animName, bool animValue)
    {
        animator.SetBool(animName, animValue);
    }



    IEnumerator SetJumpCheckEnabled()
    {
        yield return new WaitForSeconds(0.5f);
        jumpCheckEnabled = true;
    }

}
