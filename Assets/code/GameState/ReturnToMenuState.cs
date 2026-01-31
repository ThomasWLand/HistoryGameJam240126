using UnityEngine;

public class ReturnToMenuState : GameState
{
    public GameObject[] gameUI;
    public GameState menuState;
    public GameManager manager;
    private bool _destroyGrid = false;
    //Force state machine to enter this state
    public void ForceEnterState(bool destroyGrid)
    {
        this._destroyGrid = destroyGrid;
        this._stateMachine.SetState(this);
    }

    public override void EnterState()
    {
        if(this._destroyGrid)
        {
            manager.DestroyGrid();
        }
        foreach(GameObject ui in gameUI)
        {
            ui.SetActive(false);
        }
        this._setState(menuState);
    }
}