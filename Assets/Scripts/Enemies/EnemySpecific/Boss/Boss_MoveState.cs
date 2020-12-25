using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_MoveState : MoveState
{
    private Boss boss;

    public Boss_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData,Boss boss) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.boss = boss;
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
            stateMachine.ChangeState(boss.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            boss.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(boss.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
