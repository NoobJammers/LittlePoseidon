using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : State
{

    float horizontalInput;

    public GroundedState(PlayerHandler playerHandler, StateMachine stateMachine) : base(playerHandler, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = 0f;
    }


    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = Input.GetAxis("Horizontal");
    }




    public override void LogicUpdate()
    {
        base.LogicUpdate();         
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        playerHandler.MovePlayer(horizontalInput);
      
    }


    public override void Exit()
    {
        base.Exit();
        //playerHandler.ResetMoveParams();

    }

}
