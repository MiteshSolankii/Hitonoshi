using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_PlayerDetectedState : PlayerDetectedState
{
    private FinalBoss finalBoss;
    public FB_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData,FinalBoss finalBoss) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(finalBoss.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(finalBoss.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(finalBoss.moveState);
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
