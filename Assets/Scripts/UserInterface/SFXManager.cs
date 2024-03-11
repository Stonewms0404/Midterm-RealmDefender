using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    [SerializeField] SettingsScriptableObject settingsSO;
    [SerializeField] AudioMixer SFXMixer;

    private void Awake()
    {
        Projectile._SpawnSFX += SpawnSFX;
        KnightsSword._SpawnSFX += SpawnSFX;
    }
    private void OnDisable()
    {
        Projectile._SpawnSFX -= SpawnSFX;
        KnightsSword._SpawnSFX -= SpawnSFX;
    }

    void Update()
    {
        if (settingsSO.sfx)
        {
            SFXMixer.SetFloat("SFXVolume", 0.0f);
        }
        else
        {
            SFXMixer.SetFloat("SFXVolume", -80.0f);
        }
    }

    private void SpawnSFX(GameObject soundObj, Vector2 pos)
    {
        if (settingsSO.sfx)
        {
            GameObject SFX = Instantiate(soundObj, pos, Quaternion.identity, transform);
            Destroy(SFX, 2);
        }
    }
}
