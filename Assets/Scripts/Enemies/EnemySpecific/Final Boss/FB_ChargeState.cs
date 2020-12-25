using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_ChargeState : ChargeState
{
    private FinalBoss finalBoss;
    public FB_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, FinalBoss finalBoss) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(finalBoss.meleeAttackState);
        }

        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(finalBoss.lookForPlayerState);
        }

        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(finalBoss.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(finalBoss.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
