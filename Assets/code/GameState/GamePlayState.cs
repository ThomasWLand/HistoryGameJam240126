
using UnityEngine;

public class GamePlayState : GameState
{
    public GameOverState gameOverState;

    public override void EnterState()
    {
        gameOverState.SetPlayerPassed(Random.Range(0,100 ) > 50);
        Invoke("_onGameComplete", 5f);
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