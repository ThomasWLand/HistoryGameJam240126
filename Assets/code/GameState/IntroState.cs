using UnityEngine;

public class IntroState : GameState
{
    [SerializeField] GameObject[] intro, mainMenu, settingUI, background, title;
    public GameState mainMenuState;

    public override void EnterState()
    {
        SetGroupVisible(intro, true);
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, false);
        SetGroupVisible(title, false);
        Invoke("_NextState", 2);
    }

    private void _NextState()
    {
        this._setState(mainMenuState);
    }

    private void SetGroupVisible(GameObject[] group, bool isVisible)
    {
        foreach(GameObject gameObject in group)
        {
            gameObject.SetActive(isVisible);
        }
    }
}