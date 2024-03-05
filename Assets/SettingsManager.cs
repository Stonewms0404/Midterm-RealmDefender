using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private SettingsScriptableObject settingsSO;

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
    }

    public void ToggleSFX()
    {
        settingsSO.sfx = !settingsSO.sfx;
    }

    public void ToggleParticles()
    {
        settingsSO.particles = !settingsSO.particles;
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
