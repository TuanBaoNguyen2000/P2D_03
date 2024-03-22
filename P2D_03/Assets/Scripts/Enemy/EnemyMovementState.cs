using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : State
{
    public Rigidbody rigidbody;
    public Animator animator;
    public Transform target;
    public float speed;
    public EnemyAI enemyAI;

    public EnemyMovementState(EnemyAI enemyAI, Rigidbody rigidbody, Animator animator, Transform target, float speed)
    {
        this.enemyAI = enemyAI;
        this.rigidbody = rigidbody;
        this.animator = animator;
        this.target = target;
        this.speed = speed;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        Debug.Log("Enter Move");
        animator.SetTrigger("Walk");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Moving();

        if (!enemyAI.CanMoveToTarget())
        {
            Debug.Log("Enter Idle move");
            animator.SetTrigger("Idle");
            stateMachine.SetNextStateToMain();
        }
    }

    public void Moving()
    {
        Vector3 dir = Vector3.zero;
        dir.x = this.target.position.x;
        rigidbody.position = Vector3.MoveTowards(rigidbody.position, dir, this.speed);
    }
}
