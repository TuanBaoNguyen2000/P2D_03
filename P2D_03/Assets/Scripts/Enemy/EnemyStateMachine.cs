using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public override void SetMainState()
    {
        if (mainStateType == null)
            mainStateType = new IdleState();
    }

    private void OnValidate()
    {
        SetMainState();
    }
}
