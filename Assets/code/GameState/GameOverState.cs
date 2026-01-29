using TMPro;
using UnityEngine;

public class GameOverState : GameState
{
    public GameState gameEnterState;
    public TextMeshProUGUI gameOverButtonText;
    public GameObject[] gameUI;
    private bool _passed;
    public void SetPlayerPassed(bool passed)
    {
        this._passed = passed;
    }
    public override void EnterState()
    {
        gameOverButtonText.text = this._passed ? "Next Level" : "Retry Level";
        foreach(GameObject gameObject in gameUI)
        {
            gameObject.SetActive(true);
        }
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
        foreach(GameObject gameObject in gameUI)
        {
            gameObject.SetActive(false);
        }
    }
}