using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : State
{
    public Rigidbody rigidbody;
    public Animator animator;
    public Transform target;
    public float speed;

    public EnemyMovementState(Rigidbody rigidbody, Animator animator, Transform target, float speed)
    {
        this.rigidbody = rigidbody;
        this.animator = animator;
        this.target = target;
        this.speed = speed;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        Debug.Log("a");
        animator.SetTrigger("Walk");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Moving();
    }

    public void Moving()
    {
        Vector3 dir = Vector3.zero;
        dir.x = this.target.position.x;
        rigidbody.position = Vector3.MoveTowards(rigidbody.position, dir, this.speed);
    }
}
