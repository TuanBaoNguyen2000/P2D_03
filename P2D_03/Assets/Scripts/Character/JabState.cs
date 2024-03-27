using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabState : MeleeBaseState
{
    private AnimationEvents animationEvents;
    private Transform jabVFX;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        duration = 0.75f;
        animator.SetTrigger("Jab");

        animationEvents = animator.GetComponent<AnimationEvents>();
        if (animationEvents != null) animationEvents.OnCustomEvent += OnAnimationEvent;

        Debug.Log("Player Jab Fired!");
    }

    private void OnAnimationEvent(string eventName)
    {
        if (eventName == "Jab") JabVFXSpawn();
    }

    private void JabVFXSpawn()
    {
        Vector3 pos = vfxSpawnPoint.position;
        jabVFX = VFXSpawner.Instance.Spawn(VFXSpawner.JabVFX, pos, Quaternion.identity);

        float x = Mathf.Abs(jabVFX.transform.localScale.x) * animator.transform.localScale.x;
        float y = jabVFX.transform.localScale.y;
        float z = jabVFX.transform.localScale.z;
        jabVFX.transform.localScale = new Vector3(x, y, z);

        jabVFX.gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (jabVFX != null) VFXSpawner.Instance.Despawn(jabVFX);

            if (shouldCombo)
            {
                BackSlashState backSlashState = new BackSlashState();
                backSlashState.SetAnimator(animator);
                backSlashState.SetVfxSpawnPoint(vfxSpawnPoint);
                stateMachine.SetNextState(backSlashState);
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }

            animationEvents.OnCustomEvent -= OnAnimationEvent;
        }
    }
}
