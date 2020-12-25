using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState idleState { get; private set; }
    public E3_MoveState moveState { get; private set; }
    public E3_LookForPlayerState lookForPlayerState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_MeleeAttackState meleeAttackState { get; private set; }
    public E3_ChargeState chargeState { get; private set; }
    public E3_DeadState deadState { get; private set; }
   


    [SerializeField] private D_IdleState idleStateData =null;
    [SerializeField] private D_MoveState moveStateData = null;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData = null;
    [SerializeField] private D_PlayerDetected playerDetectedStateData = null;
    [SerializeField] private D_MeleeAttack meleeAttackStateData= null;
    [SerializeField] private D_ChargeState chargeStateData = null;
    [SerializeField] private D_DeadState deadStateData = null;
    

    [SerializeField] Transform meleeAttackPosition =null;

    public override void Start()
    {
        base.Start();

        idleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new E3_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        chargeState = new E3_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);
       

        stateMachine.Initialize(moveState);

    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }

        if (!isDead)
        {
            E3Audiomanager.instance.PlaySound("E3Hurt");
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

    }
}
