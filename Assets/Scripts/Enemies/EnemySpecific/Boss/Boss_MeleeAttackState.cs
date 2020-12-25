using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_MeleeAttackState : MeleeAttackState
{
    private Boss boss;
    public Boss_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData,Boss boss) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(boss.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(boss.lookForPlayerState);
            }
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
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
