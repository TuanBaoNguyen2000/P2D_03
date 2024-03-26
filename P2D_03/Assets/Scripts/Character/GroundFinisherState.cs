using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    private AnimationEvents animationEvents;
    private Transform backSlashVFX;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 3;
        duration = 0.75f;
        animator.SetTrigger("Attack" + attackIndex);

        animationEvents = animator.GetComponent<AnimationEvents>();
        if (animationEvents != null) animationEvents.OnCustomEvent += OnAnimationEvent;

        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    private void OnAnimationEvent(string eventName)
    {
        if (eventName == "BackSlash") SlashVFXSpawn();
    }

    private void SlashVFXSpawn()
    {
        Vector3 pos = vfxSpawnPoint.position;
        backSlashVFX = VFXSpawner.Instance.Spawn(VFXSpawner.BackSlashVFX, pos, Quaternion.identity);

        float x = Mathf.Abs(backSlashVFX.transform.localScale.x) * animator.transform.localScale.x;
        float y = backSlashVFX.transform.localScale.y;
        float z = backSlashVFX.transform.localScale.z;
        backSlashVFX.transform.localScale = new Vector3(x, y, z);

        backSlashVFX.gameObject.SetActive(true);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();

            if (backSlashVFX != null) VFXSpawner.Instance.Despawn(backSlashVFX);
            animationEvents.OnCustomEvent -= OnAnimationEvent;
        }
    }
}
