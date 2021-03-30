using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : GroundedState
{

    bool jump, crouch;

    public StandingState(PlayerHandler playerHandler, StateMachine stateMachine) : base(playerHandler, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void HandleInput()
    {
        base.HandleInput();

        jump = Input.GetKeyDown(KeyCode.Space);
        crouch = Input.GetKeyDown(KeyCode.LeftControl);
        
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(jump)
        {
            stateMachine.ChangeState(playerHandler.jumping);

        }else if(crouch)
        {
            stateMachine.ChangeState(playerHandler.crouching);
        }
    }

}

