using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private SettingsManager settingsManager;

    private void Start()
    {
        settingsManager = GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>();
    }

    private void OnEnable()
    {
        Tower._Death += SpawnParticles;
        Enemy._Death += SpawnParticles;
    }
    private void OnDisable()
    {
        Tower._Death -= SpawnParticles;
        Enemy._Death -= SpawnParticles;
    }

    private void SpawnParticles(GameObject particles, Vector2 objPos)
    {
        if (settingsManager.GetParticles())
        {
            GameObject particle = Instantiate(particles, objPos, Quaternion.identity, transform);
            Destroy(particle, 3);
        }
    }
}
