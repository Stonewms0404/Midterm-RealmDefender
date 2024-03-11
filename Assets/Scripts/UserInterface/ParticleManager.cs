using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] SettingsScriptableObject settingsSO;

    private void OnEnable()
    {
        Tower._SpawnParticle += SpawnParticles;
        Enemy._SpawnParticles += SpawnParticles;
        Projectile._SpawnParticles += SpawnParticles;
    }
    private void OnDisable()
    {
        Tower._SpawnParticle -= SpawnParticles;
        Enemy._SpawnParticles -= SpawnParticles;
        Projectile._SpawnParticles -= SpawnParticles;
    }

    private void SpawnParticles(GameObject particles, Vector2 objPos)
    {
        if (settingsSO.particles)
        {
            GameObject particle = Instantiate(particles, objPos, Quaternion.identity, transform);
            Destroy(particle, 3);
        }
    }
}
