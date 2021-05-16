using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Poseidon
public class PlayerHandler1 : MonoBehaviour
{
    protected enum CharState
    {
        Idle,
        Running,
        TakeOff,
        Jumping,
        ExitedJump,
        WeaponPoke,
        Dying
    }



    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator animator;
    [SerializeField] float moveMultiplier, jumpForceMultiplier;

    [SerializeField] Transform groundPos;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] bool isGrounded;
    CharState currentState;
    float horizontalInput, startTimeBetweenAttacks = 1f, timeBetweenAttacks;

    Vector3 newVelocity;
    bool didLand = true;


    private void Start()
    {
        currentState = CharState.Idle;
    }


    private void Update()
    {
        GetInput();

    }
    private void FixedUpdate()
    {
        rigidBody.velocity = newVelocity;

    }


    private void GetInput()
    {

        isGrounded = Physics2D.OverlapCircle(groundPos.position, 1f, groundLayerMask);



        //If player is not in attack mode, get input
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput != 0f)
            {
                ChangeState(CharState.Running);
                transform.localScale = new Vector3(Mathf.Sign(horizontalInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                ChangeState(CharState.Idle);
            }
        }
        else
        {
            horizontalInput = 0f;
        }

        //Prevent player from spamming attacks by keeping a delay
        if (timeBetweenAttacks > 0)
            timeBetweenAttacks -= Time.deltaTime;



        //Prevent multi-jumping
        if (isGrounded)
        {

            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.AddForce(Vector2.up * jumpForceMultiplier);
                ChangeState(CharState.TakeOff);

            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (timeBetweenAttacks <= 0)
                {
                    ChangeState(CharState.WeaponPoke);
                    timeBetweenAttacks = startTimeBetweenAttacks;

                }
            }

            if (!didLand)
            {
                ChangeState(CharState.ExitedJump);
                didLand = true;
            }



        }
        else
        {
            didLand = false;
            ChangeState(CharState.Jumping);
        }



        newVelocity = new Vector3(horizontalInput * moveMultiplier, rigidBody.velocity.y, 0f);
    }



    void ChangeState(CharState newState)
    {
        if (currentState == newState)
            return;


        switch (newState)
        {
            case CharState.Idle:
                animator.SetBool("isRunning", false);
                currentState = CharState.Idle;
                break;


            case CharState.Running:
                animator.SetBool("isRunning", true);
                currentState = CharState.Running;
                break;

            case CharState.TakeOff:
                animator.SetTrigger("takeOff");
                currentState = CharState.TakeOff;
                break;

            case CharState.Jumping:
                animator.SetBool("isJumping", true);
                currentState = CharState.Jumping;
                break;

            case CharState.ExitedJump:
                animator.SetBool("isJumping", false);
                currentState = CharState.ExitedJump;
                break;

            case CharState.WeaponPoke:
                animator.SetTrigger("weaponPoke");
                currentState = CharState.WeaponPoke;
                break;

            default:
                break;
        }


    }
}
