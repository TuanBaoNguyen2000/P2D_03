using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    public Animator animator;
    public StateMachine meleeStateMachine;

    void Start()
    {
        //meleeStateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            Debug.Log("enter");
            GroundEntryState groundEntryState = new GroundEntryState();
            groundEntryState.SetAnimator(animator);
            meleeStateMachine.SetNextState(groundEntryState);
        }
    }
}
