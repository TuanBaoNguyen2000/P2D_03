using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MeleeBaseState : State
{
    public float duration;
    protected Animator animator;
    protected bool shouldCombo = false;
    protected int attackIndex;

    protected Transform vfxSpawnPoint;

    public void SetAnimator(Animator animator)
    { 
        this.animator = animator; 
    }

    public void SetVfxSpawnPoint(Transform vfxSpawnPoint)
    {
        this.vfxSpawnPoint = vfxSpawnPoint;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButtonDown(0) && time > 0.2)
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {

    }

}
