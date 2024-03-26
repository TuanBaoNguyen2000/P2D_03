using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    public Animator animator;
    public StateMachine meleeStateMachine;
    public CharacterController characterController;
    public Transform vfxSpawnPoint;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleState))
        {
            GroundEntryState groundEntryState = new GroundEntryState();
            groundEntryState.SetAnimator(animator);
            groundEntryState.SetVfxSpawnPoint(vfxSpawnPoint);
            meleeStateMachine.SetNextState(groundEntryState);
        }

        if (Input.GetMouseButtonDown(1) && meleeStateMachine.CurrentState.GetType() == typeof(IdleState))
        {
            DashState dashState = new DashState(characterController, animator);
            meleeStateMachine.SetNextState(dashState);
        }
    }
}
