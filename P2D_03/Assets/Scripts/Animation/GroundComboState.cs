using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 2;
        duration = 0.75f;
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
                GroundFinisherState groundFinisherState = new GroundFinisherState();
                groundFinisherState.SetAnimator(animator);
                stateMachine.SetNextState(groundFinisherState);
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
