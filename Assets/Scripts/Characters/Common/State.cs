using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{

    protected PlayerHandler playerHandler;
    protected StateMachine stateMachine;



    public State(PlayerHandler playerHandler, StateMachine stateMachine)
    {
        this.playerHandler = playerHandler;
        this.stateMachine = stateMachine;
    }



    public virtual void Enter()
    {

    }


    public virtual void HandleInput()
    {

    }




    public virtual void LogicUpdate()
    {

    }


    public virtual void PhysicsUpdate()
    {

    }


    public virtual void Exit()
    {

    }



}
