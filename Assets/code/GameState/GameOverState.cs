using TMPro;
using UnityEngine;

public class GameOverState : GameState
{
    public GameState gameEnterState;
    public GameManager manager;
    public TextMeshProUGUI gameOverText;
    public GameObject[] gameOverUI;
    public void SetPlayerPassed(bool passed)
    {
        gameOverText.text = passed ? "YOU WON!" : "YOU LOST!";
        foreach(GameObject gameObject in gameOverUI)
        {
            gameObject.SetActive(true);
        }
    }
    
    public override void EnterState()
    {
        manager.DestroyGrid();
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
        foreach(GameObject gameObject in gameOverUI)
        {
            gameObject.SetActive(false);
        }
    }
}