using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : GroundedState
{

    bool crouchHeld;


    public CrouchingState(PlayerHandler playerHandler, StateMachine stateMachine) : base(playerHandler, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //playerHandler.SetAnimationBool(playerHandler.crouchParam, true);
        Debug.Log("crouching");
    }

    public override void Exit()
    {
        base.Exit();
        //playerHandler.SetAnimationBool(playerHandler.crouchParam, false);

    }

    public override void HandleInput()
    {
        base.HandleInput();
        crouchHeld = Input.GetKeyDown(KeyCode.LeftControl);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!(crouchHeld))
        {
            stateMachine.ChangeState(playerHandler.standing);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
