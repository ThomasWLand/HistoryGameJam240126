
using UnityEngine;

public class GamePlayState : GameState
{
    public GameOverState gameOverState;

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        //stub

        //Check if game has completed
    }

    public void onGameComplete(bool didWin)
    {
        this._setState(gameOverState);
    }
}