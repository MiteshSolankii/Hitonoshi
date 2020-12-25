using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_DodgeState : DodgeState
{
    private Boss boss;
    public Boss_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData,Boss boss) : base(entity, stateMachine, animBoolName, stateData)
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

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
