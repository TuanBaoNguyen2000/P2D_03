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

    [Header("Attack")]
    public bool isCanAttack = false;

    void Start()
    {

    }

    void Update()
    {
        //Move
        if (CanMoveToTarget() && enemyStateMachine.CurrentState.GetType() == typeof(IdleState))
        {
            Debug.Log("1111");
            EnemyMovementState enemyMovementState = new EnemyMovementState(this, rgbd, animator, target, speed);
            enemyStateMachine.SetNextState(enemyMovementState);
        }

        //Attack
        if (CanAttack() && enemyStateMachine.CurrentState.GetType() == typeof(IdleState) )
        {
            Debug.Log("2222");
            EnemyAttackState enemyAttackState = new EnemyAttackState(this, animator);
            enemyStateMachine.SetNextState(enemyAttackState);
        }

        FlipModel();
    }

    public bool CanMoveToTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        return distance < 10f && distance > 3f;
    }

    public bool CanAttack()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 3f;
    }

    public void FlipModel()
    {
        Vector3 direction = this.target.position - this.animator.transform.position;

        float x = -Mathf.Sign(direction.x) * Mathf.Abs(animator.transform.localScale.x);
        float y = animator.transform.localScale.y;
        float z = animator.transform.localScale.z;
        animator.transform.localScale = new Vector3(x,y,z);
    }
}
