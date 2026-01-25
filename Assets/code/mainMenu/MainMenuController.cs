using System.Dynamic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject[] intro, mainMenu, settingUI, background, game;
    void Awake()
    {
        HideAll();
        PlayIntro();
    }

    void PlayIntro()
    {
        SetGroupVisible(intro, true);
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, false);
        Invoke("ShowMainMenu", 2);
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
    
    void HideAll()
    {
        SetGroupVisible(intro, false);
        SetGroupVisible(mainMenu, false);
        SetGroupVisible(settingUI, false);
        SetGroupVisible(background, false);
        SetGroupVisible(game, false);
    }
    
    public void PlayGame()
    {
        // stub
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetGroupVisible(GameObject[] group, bool isVisible)
    {
        foreach(GameObject gameObject in group)
        {
            gameObject.SetActive(isVisible);
        }
    }
}
