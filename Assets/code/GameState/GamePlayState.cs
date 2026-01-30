
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

    private void _onGameComplete()
    {
        this._setState(gameOverState);
    }
}