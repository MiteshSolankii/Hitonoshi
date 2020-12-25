using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;



    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    #region Audio

    void ArcherBowSound()
    {
        E2Audiomanager.instance.PlaySound("E2RangedAttack");
    }
    void ArcherMeleeSound()
    {
        E2Audiomanager.instance.PlaySound("E2MeleeAttack");
    }
    void SkullMeleeSound()
    {
        E3Audiomanager.instance.PlaySound("E3Attack");
    }
    void BossAttackSound()
    {
        BossAudiomanager.instance.PlaySound("BossAttack");
    }
    void FinalBossAttackSound()
    {
        FinalBossAudioManager.instance.PlaySound("FbAttack");
    }

    #endregion
}
