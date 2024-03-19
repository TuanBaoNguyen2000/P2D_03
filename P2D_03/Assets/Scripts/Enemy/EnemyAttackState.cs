using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public float timer;
    public Animator animator;

    public EnemyAttackState(Animator animator)
    {
        this.animator = animator;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        timer = 2f;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime > timer)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Enemy Attack");
        }

    }
}
