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
    private bool canShoot = true;

    private void Start()
    {
        enemyFolder = GameObject.FindGameObjectWithTag("EnemyFolder").GetComponent<EnemyFolder>();
    }

    private void Update()
    {
        if (towerScript.GetIfWaveIsRunning())
        {
            int enemyNum = enemyFolder.GetNumEnemiesInRange(this.transform.position, towerScript.GetSightRange());
            if (enemyNum > 0)
            {
                if (useTimer <= towerScript.GetUseTime() && !canShoot)
                    useTimer += Time.deltaTime;
                else
                {
                    canShoot = true;
                }
                Attack();
            }
        }
    }

    private void Attack()
    {
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
        useTimer = 0.0f;
        canShoot = false;
    }
}
