using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopMove : MonoBehaviour
{
    private FinalBoss boss;
        void Start()
    {
        boss = FindObjectOfType<FinalBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            boss.stateMachine.ChangeState(boss.idleState);
        }
    }
}
