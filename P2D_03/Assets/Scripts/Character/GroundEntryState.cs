using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    private AnimationEvents animationEvents;
    private Transform slashVFX;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 1;
        duration = 0.75f;
        animator.SetTrigger("Attack" + attackIndex);

        animationEvents = animator.GetComponent<AnimationEvents>();
        if (animationEvents != null) animationEvents.OnCustomEvent += OnAnimationEvent; 

        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    private void OnAnimationEvent(string eventName)
    {
        if (eventName == "Slash") SlashVFXSpawn();
    }

    private void SlashVFXSpawn()
    {
        Vector3 pos = vfxSpawnPoint.position;
        slashVFX = VFXSpawner.Instance.Spawn(VFXSpawner.SlashVFX, pos, Quaternion.identity);

        float x = Mathf.Abs(slashVFX.transform.localScale.x) * animator.transform.localScale.x;
        float y = slashVFX.transform.localScale.y;
        float z = slashVFX.transform.localScale.z;
        slashVFX.transform.localScale = new Vector3(x, y, z);

        slashVFX.gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (slashVFX != null) VFXSpawner.Instance.Despawn(slashVFX);

            if (shouldCombo)
            {
                GroundComboState groundComboState = new GroundComboState();
                groundComboState.SetAnimator(animator);
                groundComboState.SetVfxSpawnPoint(vfxSpawnPoint);
                stateMachine.SetNextState(groundComboState);
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }

            animationEvents.OnCustomEvent -= OnAnimationEvent;
        }
    }
}
