using UnityEngine;

public class IntroState : GameState
{
    [SerializeField] GameObject[] intro, mainMenu, settingUI, background;
    public GameState mainMenuState;

    public override void EnterState()
    {
        SetGroupVisible(intro, true);
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, false);
        Invoke("_NextState", 2);
    }

    private void _NextState()
    {
        GameStateMachine machine = this._stateMachine;
        machine.SetState(this.mainMenuState);
    }

    private void SetGroupVisible(GameObject[] group, bool isVisible)
    {
        foreach(GameObject gameObject in group)
        {
            gameObject.SetActive(isVisible);
        }
    }
}