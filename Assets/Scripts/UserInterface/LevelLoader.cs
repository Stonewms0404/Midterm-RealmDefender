using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private SettingsManager settingsManager;
    private static LevelLoader instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        settingsManager = GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>();
    }

    public void GoToScene(int sceneIndex)
    {
        settingsManager.SetSceneIndex(sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
