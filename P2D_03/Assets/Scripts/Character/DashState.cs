using Assets.HeroEditor.Common.CharacterScripts;
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

    private Transform flashVFX;

    public DashState(CharacterController controller, Animator animator)
    {
        this.characterController = controller;
        this.animator = animator;
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

    private void DashVFXSpawn()
    {
        Vector3 pos = characterController.transform.position + new Vector3(3, 0, 0) * animator.transform.localScale.x;
        flashVFX = VFXSpawner.Instance.Spawn(VFXSpawner.FlashVFX, pos, Quaternion.identity);
        flashVFX.gameObject.SetActive(true);
        flashVFX.transform.localScale = new Vector3(3, 1, 1) * animator.transform.localScale.x;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            VFXSpawner.Instance.Despawn(flashVFX);
            stateMachine.SetNextStateToMain();
        }
        else
        {
            if (fixedtime < delayTime) return;
            if (flashVFX == null) DashVFXSpawn();
            Vector3 dir = Vector3.right * animator.transform.localScale.x;
            characterController.Move(dashSpeed * dir * Time.deltaTime);
        }
    }

    private float Duration()
    {
        float duration;
        duration = dashLenght / dashSpeed + delayTime;
        return duration > 0 ? duration : delayTime;
    }
}
