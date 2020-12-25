using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_IdleState : IdleState
{
    private FinalBoss finalBoss;
    public FB_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData,FinalBoss finalBoss) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.finalBoss = finalBoss;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(finalBoss.playerDetectedState);
        }

        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(finalBoss.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

   
}
