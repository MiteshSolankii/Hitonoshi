using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_MoveState : MoveState
{
    private FinalBoss finalBoss;
    public FB_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData,FinalBoss finalBoss) : base(entity, stateMachine, animBoolName, stateData)
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

        else if (isDetectingWall || !isDetectingLedge)
        {
            finalBoss.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(finalBoss.idleState);
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
