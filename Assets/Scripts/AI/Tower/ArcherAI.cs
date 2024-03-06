using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    [SerializeField]
    private Tower towerScript;

    private EnemyFolder enemyFolder;
    private float useTimer;
    private bool canShoot;
    private GameObject enemy;

    private void Start()
    {
        enemyFolder = GameObject.FindGameObjectWithTag("EnemyFolder").GetComponent<EnemyFolder>();
    }

    private void Update()
    {
        if (towerScript.GetIfWaveIsRunning())
        {
            enemy = enemyFolder.GetClosestEnemy((Vector2)transform.position, true);
            if (enemy != null)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (useTimer <= towerScript.GetUseTime() && !canShoot)
            useTimer += Time.deltaTime;
        else
        {
            canShoot = true;
        }

        if (canShoot)
        {
            Shoot();
            ResetShooting();
        }
    }

    private void Shoot()
    {
        towerScript.ShootProjectile(transform, 0);
    }

    private void ResetShooting()
    {
        canShoot = false;
        useTimer = 0.0f;
    }
}
