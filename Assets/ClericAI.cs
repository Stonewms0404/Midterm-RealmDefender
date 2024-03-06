using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ClericAI : MonoBehaviour
{
    [SerializeField]
    private Tower towerScript;

    private SpawnTowers spawnTowers;
    private float useTimer;
    private int numberOfTowersInRange;
    private bool canHeal;

    private void Start()
    {
        spawnTowers = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<SpawnTowers>();
    }

    private void Update()
    {
        if (towerScript.GetIfWaveIsRunning())
        {
            if (useTimer <= towerScript.GetUseTime() && !canHeal)
                useTimer += Time.deltaTime;
            else
                Heal();
        }
    }

    private void Heal()
    {
        if (canHeal)
            Shoot();
        else
        {
            int numberOfTowersInRange = spawnTowers.FindNumTowers();

            if (numberOfTowersInRange > 1)
                canHeal = true;
        }

        ResetShooting();
    }

    private void Shoot()
    {
        for (int i = 0; i < numberOfTowersInRange - 1; i++)
        {
            towerScript.ShootProjectile(transform, 0);
        }
    }

    private void ResetShooting()
    {
        canHeal = false;
        useTimer = 0.0f;
    }
}
