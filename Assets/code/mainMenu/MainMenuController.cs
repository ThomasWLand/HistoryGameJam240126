using System.Dynamic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject intro, mainMenu, settingUI, background;
    void Awake()
    {
        HideAll();
        PlayIntro();
    }

    void PlayIntro()
    {
        intro.SetActive(true);
        mainMenu.SetActive(false);
        settingUI.SetActive(false);

        Invoke("ShowMainMenu", 2);
    }

    public void ShowMainMenu()
    {
        intro.SetActive(false);
        mainMenu.SetActive(true);
        settingUI.SetActive(false);
        background.SetActive(true);
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settingUI.SetActive(true);
    }
    
    void HideAll()
    {
        intro.SetActive(false);
        mainMenu.SetActive(false);
        settingUI.SetActive(false);
        background.SetActive(false);
    }
    
    public void PlayGame()
    {
        // stub
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
