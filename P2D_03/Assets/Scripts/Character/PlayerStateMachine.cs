using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public override void SetMainState()
    {
        if (mainStateType == null)
        {
            if (customName == "Combat")
            {
                mainStateType = new IdleState();
            }
        }
    }

    private void OnValidate()
    {
        SetMainState();
    }
}