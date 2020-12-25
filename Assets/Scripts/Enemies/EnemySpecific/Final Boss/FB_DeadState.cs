using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_DeadState : DeadState
{
    private FinalBoss finalBoss;
    public FB_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, FinalBoss finalBoss) : base(entity, stateMachine, animBoolName, stateData)
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
       FinalBossAudioManager.instance.PlaySound("FbDie");
        
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

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
