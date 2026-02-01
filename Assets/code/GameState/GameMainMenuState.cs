using UnityEngine;

public class GameMainMenuState : GameState
{
    [SerializeField] GameObject[] intro, mainMenu, settingUI, settingBackToMenuUI, volumeUI, background, title;
    public GameState gameEnterState;
    public CursorController cursorController;

    public override void EnterState()
    {
        cursorController.SetSprite(true);
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        SetGroupVisible(intro, false);
        SetGroupVisible(mainMenu, true);
        SetGroupVisible(settingUI, true);
        SetGroupVisible(background, true);
        SetGroupVisible(title, true);
        SetGroupVisible(settingBackToMenuUI, false);
        SetGroupVisible(volumeUI, false);
    }

    public void ShowSettings()
    {
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, true);
        SetGroupVisible(settingBackToMenuUI, true);
        SetGroupVisible(volumeUI, true);
    }

    public void PlayGame()
    {
        this._setState(this.gameEnterState);
    }

    public override void ExitState()
    {
        SetGroupVisible(intro, false);
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, false);
        SetGroupVisible(title, false);
        SetGroupVisible(settingBackToMenuUI, false);
        SetGroupVisible(volumeUI, false);
    }

    private void SetGroupVisible(GameObject[] group, bool isVisible)
    {
        foreach(GameObject gameObject in group)
        {
            gameObject.SetActive(isVisible);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}