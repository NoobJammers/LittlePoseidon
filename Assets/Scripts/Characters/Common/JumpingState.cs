using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    bool grounded;
    public JumpingState(PlayerHandler playerHandler, StateMachine stateMachine) : base(playerHandler, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        grounded = false;
        playerHandler.SetBoolAnimations("isJumping", true);
        Jump(); 

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
        {
            stateMachine.ChangeState(playerHandler.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        grounded = playerHandler.CheckCollisionOverlap();
    }



    private void Jump()
    {
        playerHandler.PlayerJump();
    }

    public override void Exit()
    {
        base.Exit();
        playerHandler.SetBoolAnimations("isJumping", false);
    }




}
