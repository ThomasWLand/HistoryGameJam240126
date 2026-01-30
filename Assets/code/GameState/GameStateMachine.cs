using System;
using UnityEditor;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameStateMachine _stateMachine;
    public virtual void InitState(GameStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }
    public virtual void EnterState(){}
    public virtual void UpdateState(){}
    public virtual void ExitState(){}

    protected void _setState(GameState newState)
    {
        GameStateMachine machine = this._stateMachine;
        if(machine.GetState() == this)
        {
            machine.SetState(newState);
        }
    }
}

public class GameStateMachine : MonoBehaviour
{
    public GameState entryState;
    public GameState[] allStates;
    private GameState _currState = null;

    void Start()
    {
        foreach(GameState state in allStates)
        {
            state.InitState(this);
        }
        this.SetState(entryState);
    }

    void Update()
    {
        GameState currState = this._currState;
        if(currState != null)
        {
            currState.UpdateState();
        }
    }

    public void SetState(GameState newState)
    {
        GameState currState = this._currState;
        if(currState == newState || newState == null)
        {
            return;
        }
        if(currState != null)
        {
            currState.ExitState();
        }
        this._currState = newState;
        newState.EnterState();
        Debug.Log("state changed: " + this._currState.gameObject.name);
    }

    public GameState GetState() => this._currState;
}
