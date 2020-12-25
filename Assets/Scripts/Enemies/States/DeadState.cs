using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;

   protected int rand = Random.Range(0, 9);
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
       
      
        if(rand <= 3)
        {
            GameObject.Instantiate(stateData.dropItems[Random.Range(0, stateData.dropItems.Length)],entity.aliveGO.transform.position,entity.aliveGO.transform.rotation);
        }

         GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGO.transform.position, stateData.deathBloodParticle.transform.rotation);
         GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGO.transform.position, stateData.deathChunkParticle.transform.rotation);
         entity.gameObject.SetActive(false);
       

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
