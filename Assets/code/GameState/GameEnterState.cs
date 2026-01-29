using System;
using UnityEngine;

public class GameEnterState : GameState
{
    public GameManager gameManager;
    public GameState gamePlayState;
    public GameObject[] gameUI;

    public override void EnterState()
    {
        foreach(GameObject gameObject in gameUI)
        {
            gameObject.SetActive(true);
        }
        gameManager.BeginGame();
        Invoke("_startPlayState", 0.5f);
    }

    private void _startPlayState()
    {
        this._stateMachine.SetState(gamePlayState);
    }
}