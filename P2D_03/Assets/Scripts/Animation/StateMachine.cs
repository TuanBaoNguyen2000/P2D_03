using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string customName;

    private State mainStateType;

    public State CurrentState { get; private set; }
    private State nextState;

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
        {
            Debug.Log(nextState.ToString() + " 3");
            SetState(nextState);
        }

        if (CurrentState != null)
            CurrentState.OnUpdate();
    }

    private void SetState(State _newState)
    {
        Debug.Log("setnext");
        nextState = null;
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    private void LateUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnLateUpdate();
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }

    private void Awake()
    {
        Debug.Log("awake");
        SetNextStateToMain();

    }


    private void OnValidate()
    {
        Debug.Log("Valid");
        if (mainStateType == null)
        {
            if (customName == "Combat")
            {
                mainStateType = new IdleCombatState();
            }
        }
    }
}