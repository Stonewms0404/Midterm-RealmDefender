using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static event Action<bool, bool, bool> AdjustSettings;
    [SerializeField]
    private SettingsScriptableObject settingsSO;

    private SettingsManager instance;

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
        gameObject.SetActive(true);
    }

    public int GetSceneIndex()
    {
        return settingsSO.sceneIndex;
    }
    public void SetSceneIndex(int value)
    {
        settingsSO.sceneIndex = value;
    }

    public void ToggleMusic()
    {
        settingsSO.music = !settingsSO.music;
        AdjustSettings(settingsSO.music, settingsSO.sfx, settingsSO.particles);
    }

    public void ToggleSFX()
    {
        settingsSO.sfx = !settingsSO.sfx;
        AdjustSettings(settingsSO.music, settingsSO.sfx, settingsSO.particles);
    }

    public void ToggleParticles()
    {
        settingsSO.particles = !settingsSO.particles;
        AdjustSettings(settingsSO.music, settingsSO.sfx, settingsSO.particles);
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
