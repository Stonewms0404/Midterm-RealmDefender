using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUIobj, startMenuUIobj, creditsMenuUIobj, instructionsMenuUIobj, logoObj;

    private LevelLoader levelLoader;
    private SettingsManager settingsManager;


    void Start()
    {
        ChangeMenuToMainMenu();
        settingsManager = GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    public void StartGame(int choice)
    {
        settingsManager.SetSceneIndex(choice);
        levelLoader.GoToScene(choice);
    }

    public void ChangeMenuToMainMenu()
    {
        mainMenuUIobj.SetActive(true);
        logoObj.SetActive(true);
        startMenuUIobj.SetActive(false);
        creditsMenuUIobj.SetActive(false);
        instructionsMenuUIobj.SetActive(false);
    }

    public void ChangeMenutoStartMenu()
    {
        mainMenuUIobj.SetActive(false);
        logoObj.SetActive(false);
        startMenuUIobj.SetActive(true);
        creditsMenuUIobj.SetActive(false);
        instructionsMenuUIobj.SetActive(false);
    }

    public void ChangeMenuToCredits()
    {
        mainMenuUIobj.SetActive(false);
        logoObj.SetActive(false);
        startMenuUIobj.SetActive(false);
        creditsMenuUIobj.SetActive(true);
        instructionsMenuUIobj.SetActive(false);
    }

    public void ChangeMenuToInstructions()
    {
        mainMenuUIobj.SetActive(false);
        logoObj.SetActive(false);
        startMenuUIobj.SetActive(false);
        creditsMenuUIobj.SetActive(false);
        instructionsMenuUIobj.SetActive(true);
    }

    public void QuitGame()
    {
        levelLoader.QuitApp();
    }
}
