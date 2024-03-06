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
    private Tower[] towers;
    private GameObject[] towersInRange;

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
        else
            Debug.Log("Game not running");
    }

    private void FindNumTowersInRange()
    {
        numberOfTowersInRange = 0;
        float currentDist = towerScript.GetAbsDistance(towers[0].transform.position);

        for (int i = 0; i < towers.Length; i++)
        {
            float dist = towerScript.GetAbsDistance(towers[i].transform.position);
            if (dist <= currentDist && dist <= towerScript.GetSightRange() && towers[i].transform.position != transform.position)
            {
                currentDist = dist;
                numberOfTowersInRange++;
            }
        }
        canHeal = numberOfTowersInRange != 0;
    }

    private void Heal()
    {
        if (canHeal)
            Shoot();
        else
        {
            ResetShooting();
            towers = spawnTowers.GetTowers();

            if (towers != null)
                FindNumTowersInRange();
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < numberOfTowersInRange; i++)
        {
            towerScript.ShootProjectile(transform, 0);
        }
        ResetShooting();
    }

    private void ResetShooting()
    {
        canHeal = false;
        useTimer = 0.0f;
    }
}
