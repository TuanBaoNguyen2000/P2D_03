using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 1;
        duration = 1f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                GroundComboState groundComboState = new GroundComboState();
                groundComboState.SetAnimator(animator);
                stateMachine.SetNextState(groundComboState);
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}