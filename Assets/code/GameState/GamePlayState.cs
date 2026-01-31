
using UnityEngine;

public class GamePlayState : GameState
{
    public GameManager manager;
    public GameOverState gameOverState;
    public GameObject[] playUI;

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
        gameOverState.SetPlayerPassed(didWin);
        this._setState(gameOverState);
    }

    public override void ExitState()
    {
        foreach(GameObject gameObject in playUI)
        {
            gameObject.SetActive(false);
        }
    }
}