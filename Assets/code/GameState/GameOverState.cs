using TMPro;
using UnityEngine;

public class GameOverState : GameState
{
    public GameState gameEnterState;
    public void SetPlayerPassed(bool passed)
    {
    }
    public override void EnterState()
    {
    }

    public void EnterNextLevel()
    {
        if(this._stateMachine.GetState() == this)
        {
            this._setState(gameEnterState);
        }
    }

    public override void ExitState()
    {
    }
}