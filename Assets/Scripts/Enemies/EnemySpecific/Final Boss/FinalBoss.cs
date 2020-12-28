using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : Entity
{
    public FB_IdleState idleState { get; private set; }
    public FB_MoveState moveState { get; private set; }
    public FB_PlayerDetectedState playerDetectedState { get; private set; }
    public FB_MeleeAttackState meleeAttackState { get; private set; }
    public FB_LookForPlayerState lookForPlayerState { get; private set; }
    public FB_DeadState deadState { get; private set; }
    public FB_ChargeState chargeState { get; private set; }
   
    



    [SerializeField] D_IdleState idleStateData = null;
    [SerializeField] D_MoveState moveStateData = null;
    [SerializeField] D_PlayerDetected playerDetectedStateData = null;
    [SerializeField] private D_MeleeAttack meleeAttackStateData = null;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData = null;
    [SerializeField] private D_DeadState deadStateData = null;
    [SerializeField] D_ChargeState chargeStateData = null;
   


    [SerializeField] Transform meleeAttackPosition = null;
    public Slider bossHealthSlider = null;
    public GameObject npcKing = null;

    public override void Start()
    {
        base.Start();
        
        idleState = new FB_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new FB_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new FB_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new FB_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new FB_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new FB_DeadState(this, stateMachine, "dead", deadStateData, this);
        chargeState = new FB_ChargeState(this, stateMachine, "charge", chargeStateData, this);

        stateMachine.Initialize(moveState);
    }
    public override void Update()
    {
        base.Update();

        bossHealthSlider.value = currentHealth;
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            npcKing.SetActive(true);
            stateMachine.ChangeState(deadState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
        if (!isDead)
        {
          FinalBossAudioManager.instance.PlaySound("FbHurt");
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    private void OnDisable()
    {
        if(bossHealthSlider != null)
        bossHealthSlider.gameObject.SetActive(false);
    }
}
