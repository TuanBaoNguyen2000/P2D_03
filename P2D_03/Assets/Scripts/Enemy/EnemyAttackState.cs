using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public float timer;
    public Animator animator;
    public EnemyAI enemyAI;

    public EnemyAttackState(EnemyAI enemyAI, Animator animator)
    {
        this.enemyAI = enemyAI;
        this.animator = animator;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        Debug.Log("Enter Attack");
        timer = 2f;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            animator.SetTrigger("Attack");
            timer = 2f;
            Debug.Log("EAttack");
        }

        if (!enemyAI.CanAttack())
        {
            Debug.Log("Enter Idle attack");
            stateMachine.SetNextStateToMain();
        }
    }
}
