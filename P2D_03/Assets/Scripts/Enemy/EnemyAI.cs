using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody rgbd;
    public Animator animator;
    public StateMachine enemyStateMachine;

    [Header("Movement")]
    public float speed;
    public Transform target;

    void Start()
    {

    }

    void Update()
    {
        //Move
        if (CanMoveToTarget() && enemyStateMachine.CurrentState.GetType() == typeof(IdleState))
        {
            EnemyMovementState enemyMovementState = new EnemyMovementState(rgbd, animator, target, speed);
            enemyStateMachine.SetNextState(enemyMovementState);
        }
        else if (!CanMoveToTarget() && enemyStateMachine.CurrentState.GetType() != typeof(IdleState))
        {
            animator.SetTrigger("Idle");
            enemyStateMachine.SetNextState(new IdleState());
        }

        //Attack
        //if (CanAttack() && enemyStateMachine.CurrentState.GetType() == typeof(IdleState))
        //{
        //    EnemyAttackState enemyAttackState = new EnemyAttackState(animator);
        //    enemyStateMachine.SetNextState(enemyAttackState);
        //}
        //else if(!CanAttack() && enemyStateMachine.CurrentState.GetType() != typeof(IdleState))
        //{
        //    animator.SetTrigger("Idle");
        //    enemyStateMachine.SetNextState(new IdleState());
        //}
    }

    private bool CanMoveToTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        return distance < 10f && distance > 5f;
    }

    private bool CanAttack()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 3f;
    }
}
