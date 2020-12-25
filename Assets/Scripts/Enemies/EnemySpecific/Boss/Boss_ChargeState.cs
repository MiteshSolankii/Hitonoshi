using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ChargeState : ChargeState
{
    private Boss boss;
    public Boss_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData,Boss boss) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(boss.meleeAttackState);
        }

        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(boss.lookForPlayerState);
        }

        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(boss.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(boss.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

    


