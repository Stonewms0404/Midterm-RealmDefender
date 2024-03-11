using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Options : MonoBehaviour
{
    [SerializeField] Toggle musicToggle, sfxToggle, particlesToggle, fullscreenToggle;
    [SerializeField] SettingsScriptableObject settingsSO;

    private void Awake()
    {
        musicToggle.isOn = settingsSO.music;
        sfxToggle.isOn = settingsSO.sfx;
        particlesToggle.isOn = settingsSO.particles;
        fullscreenToggle.isOn = settingsSO.fullscreen;
        ToggleFullscreen(settingsSO.fullscreen);
    }

    public void ToggleMusic(bool value)
    {
        settingsSO.music = value;
    }

    public void ToggleSFX(bool value)
    {
        settingsSO.sfx = value;
    }

    public void ToggleParticles(bool value)
    {
        settingsSO.particles = value;
    }

    public void ToggleFullscreen(bool value)
    {
        settingsSO.fullscreen = value;
        Screen.fullScreen = settingsSO.fullscreen;
    }
}
