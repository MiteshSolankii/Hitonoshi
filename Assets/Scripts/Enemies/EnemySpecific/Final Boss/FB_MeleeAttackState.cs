using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_MeleeAttackState : MeleeAttackState
{
    private FinalBoss finalBoss;
    public FB_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, FinalBoss finalBoss) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
                stateMachine.ChangeState(finalBoss.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(finalBoss.lookForPlayerState);
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
