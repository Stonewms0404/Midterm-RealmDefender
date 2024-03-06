using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManger : MonoBehaviour
{
    [SerializeField]
    private GameObject levelLoaderobj, settingsManagerobj;

    // Start is called before the first frame update
    void Start()
    {
        bool isSettingsManager = GameObject.FindGameObjectWithTag("SettingsManager");
        bool isLevelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
        if (!isSettingsManager)
        {
            DontDestroyOnLoad(levelLoaderobj);
        }
        if (!isLevelLoader)
        {
            DontDestroyOnLoad(settingsManagerobj);
        }
    }
}
