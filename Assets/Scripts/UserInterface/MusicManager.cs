using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SettingsScriptableObject settingsSO;
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource deathMusic;
    [SerializeField] AudioSource victoryMusic;

    private void Awake()
    {
        Health.GameOver += PlayDeathMusic;
        WavesManager._GameFinished += PlayVictoryMusic;
        levelMusic.Play();
    }
    private void OnDestroy()
    {
        Health.GameOver -= PlayDeathMusic;
        WavesManager._GameFinished -= PlayVictoryMusic;
    }

    void Update()
    {
        if (settingsSO.music)
        {
            musicMixer.SetFloat("MusicVolume", 0.0f);
        }
        else
        {
            musicMixer.SetFloat("MusicVolume", -80.0f);
        }
    }

    private void PlayDeathMusic()
    {
        levelMusic.Pause();
        deathMusic.Play();
    }

    private void PlayVictoryMusic()
    {
        levelMusic.Pause();
        victoryMusic.Play();
    }
}
