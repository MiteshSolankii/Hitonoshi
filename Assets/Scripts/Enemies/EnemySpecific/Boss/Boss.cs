using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    public Boss_IdleState idleState { get; private set; }
    public Boss_MoveState moveState { get; private set; }
    public Boss_PlayerDetectedState playerDetectedState { get; private set; }
    public Boss_MeleeAttackState meleeAttackState { get; private set; }
    public Boss_LookForPlayerState lookForPlayerState { get; private set; }
    public Boss_DeadState deadState { get; private set; }
    public Boss_ChargeState chargeState { get; private set; }
    


    [SerializeField]private D_MoveState moveStateData = null;
    [SerializeField]private D_IdleState idleStateData = null;
    [SerializeField] private D_PlayerDetected playerDetectedStateData = null;
    [SerializeField]private D_MeleeAttack meleeAttackStateData =null;
    [SerializeField]private D_LookForPlayer lookForPlayerStateData =null;
    [SerializeField]private D_DeadState deadStateData = null;
    [SerializeField]private D_ChargeState chargeStateData = null;
   

    [SerializeField] private Transform meleeAttackPosition = null;
   

    public override void Start()
    {
        base.Start();

        idleState = new Boss_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Boss_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Boss_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new Boss_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new Boss_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new Boss_DeadState(this, stateMachine, "dead", deadStateData, this);
        chargeState = new Boss_ChargeState(this, stateMachine, "charge", chargeStateData, this);
      

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
         BossAudiomanager.instance.PlaySound("BossHurt");
        }

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
