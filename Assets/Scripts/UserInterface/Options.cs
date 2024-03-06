using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    private Toggle musicToggle, sfxToggle, particlesToggle;

    private void OnEnable()
    {
        SettingsManager.AdjustSettings += AdjustToggle;
    }
    private void OnDisable()
    {
        SettingsManager.AdjustSettings -= AdjustToggle;
    }

    private void AdjustToggle(bool music, bool sfx, bool particles)
    {
        musicToggle.isOn = music;
        sfxToggle.isOn = sfx;
        particlesToggle.isOn = particles;
    }
}
