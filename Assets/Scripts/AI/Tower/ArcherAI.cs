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
    private Vector2 enemy;

    private void Start()
    {
        enemyFolder = GameObject.FindGameObjectWithTag("EnemyFolder").GetComponent<EnemyFolder>();
    }

    private void Update()
    {
        enemy = enemyFolder.GetClosestEnemy((Vector2)transform.position, towerScript.GetSightRange());
        if (enemy != Vector2.zero)
        {
            Attack();
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

    public Vector2 GetClosestEnemy()
    {
        return enemy;
    }

    private void Shoot()
    {
        towerScript.ShootProjectile(transform);
    }

    private void ResetShooting()
    {
        canShoot = false;
        useTimer = 0.0f;
    }
}
