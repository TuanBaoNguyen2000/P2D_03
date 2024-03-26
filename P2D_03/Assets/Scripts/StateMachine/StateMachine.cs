using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public string customName;
    protected State mainStateType;

    public State CurrentState { get; private set; }
    protected State nextState;

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
        }

        if (CurrentState != null)
            CurrentState.OnUpdate();
    }

    protected void SetState(State _newState)
    {
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

    private void Start()
    {
        SetNextStateToMain();
    }

    public abstract void SetMainState();
}