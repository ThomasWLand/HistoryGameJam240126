using UnityEngine;

public class GameMainMenuState : GameState
{
    [SerializeField] GameObject[] intro, mainMenu, settingUI, background;
    public GameState gameEnterState;

    public override void EnterState()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        SetGroupVisible(intro, false);
        SetGroupVisible(mainMenu, true);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, true);
    }

    public void ShowSettings()
    {
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, true);
    }

    public void PlayGame()
    {
        GameStateMachine machine = this._stateMachine;
        machine.SetState(this.gameEnterState);
    }

    public override void ExitState()
    {
        SetGroupVisible(intro, false);
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, false);
    }

    private void SetGroupVisible(GameObject[] group, bool isVisible)
    {
        foreach(GameObject gameObject in group)
        {
            gameObject.SetActive(isVisible);
        }
    }
}