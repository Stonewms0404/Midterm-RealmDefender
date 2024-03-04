using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsManagerobj, levelLoaderobj;

    private LevelLoader levelLoader;
    private SettingsManager settingsManager;


    void Start()
    {
        settingsManagerobj = GameObject.FindGameObjectWithTag("SettingsManager");
        levelLoaderobj = GameObject.FindGameObjectWithTag("LevelLoader");
        if (!settingsManagerobj)
        {
            DontDestroyOnLoad(settingsManagerobj);
            settingsManager = settingsManagerobj.GetComponent<SettingsManager>();
        }
        else
        {
            settingsManager = settingsManagerobj.GetComponent<SettingsManager>();
        }
        if (!levelLoaderobj)
        {
            DontDestroyOnLoad(levelLoaderobj);
            levelLoader = levelLoaderobj.GetComponent<LevelLoader>();
        }
        else
        {
            levelLoader = levelLoaderobj.GetComponent<LevelLoader>();
        }
    }


}
