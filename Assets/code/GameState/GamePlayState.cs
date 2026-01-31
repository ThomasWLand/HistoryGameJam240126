
using UnityEngine;

public class GamePlayState : GameState
{
    public GameManager manager;
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
        manager.SetIsPlaying(false);
        this._setState(gameOverState);
    }
}