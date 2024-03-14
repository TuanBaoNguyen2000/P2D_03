using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public float duration;
    public float delayTime;
    public float dashSpeed;
    public float dashLenght;
    protected Animator animator;
    protected CharacterController characterController;
    protected GameObject flash;

    public DashState(CharacterController controller, Animator animator, GameObject flash)
    {
        this.characterController = controller;
        this.animator = animator;
        this.flash = flash;
        flash.transform.position = characterController.transform.position + new Vector3(3,0,0) * animator.transform.localScale.x;
        flash.transform.localScale = new Vector3(3,1,1) * animator.transform.localScale.x;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Dash
        dashLenght = 5f;
        delayTime = 0.5f;
        dashSpeed = 30f;
        duration = Duration();

        animator.SetTrigger("Dash");
        Debug.Log("Player Dash");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            flash.SetActive(false);
            stateMachine.SetNextStateToMain();
        }
        else
        {
            if (fixedtime < delayTime) return;
            flash.SetActive(true);
            Vector3 dir = Vector3.right * animator.transform.localScale.x;
            characterController.Move(dashSpeed * dir * Time.deltaTime);
        }
    }

    private float Duration()
    {
        float duration = 0;
        duration = dashLenght / dashSpeed + delayTime;
        return duration > 0 ? duration : delayTime;
    }
}
