using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] SettingsScriptableObject settingsSO;

    private static SettingsManager instance;

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

    public int GetSceneIndex()
    {
        return settingsSO.sceneIndex;
    }
    public void SetSceneIndex(int value)
    {
        settingsSO.sceneIndex = value;
    }

    public bool GetMusic()
    {
        return settingsSO.music;
    }
    public bool GetSFX()
    {
        return settingsSO.sfx;
    }
    public bool GetParticles()
    {
        return settingsSO.particles;
    }
}
