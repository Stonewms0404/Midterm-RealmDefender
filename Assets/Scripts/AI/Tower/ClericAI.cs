using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericAI : MonoBehaviour
{
    [SerializeField]
    private Tower towerScript;

    private SpawnTowers spawnTowers;
    private float useTimer;
    private int numberOfTowersInRange;
    private bool canHeal = true;

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
        numberOfTowersInRange = spawnTowers.FindNumTowers(transform.position, towerScript.GetSightRange());

        if (numberOfTowersInRange > 1)
            canHeal = true;

        if (canHeal)
            Shoot();

    }

    private void Shoot()
    {
        ResetShooting();
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
