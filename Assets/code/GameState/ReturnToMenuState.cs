using UnityEngine;

public class ReturnToMenuState : GameState
{
    public GameObject[] gameUI;
    public GameState menuState;
    public GameManager manager;
    //Force state machine to enter this state
    public void ForceEnterState()
    {
        this._stateMachine.SetState(this);
    }

    public override void EnterState()
    {
        manager.DestroyGrid();
        foreach(GameObject ui in gameUI)
        {
            ui.SetActive(false);
        }
        this._setState(menuState);
    }
}