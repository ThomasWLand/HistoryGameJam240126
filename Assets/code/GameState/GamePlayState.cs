
public class GamePlayState : GameState
{
    public GameState gameOverState;
    public override void UpdateState()
    {
        //stub

        //Check if game has completed
    }

    private void _onGameComplete()
    {
        this._stateMachine.SetState(gameOverState);
    }
}