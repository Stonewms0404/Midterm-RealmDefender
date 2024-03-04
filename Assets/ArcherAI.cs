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
        enemy = enemyFolder.GetEnemiesInRange((Vector2)transform.position, towerScript.GetSightRange());
        if (enemy != null)
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

        if (Input.GetAxisRaw("Fire1") != 0 && canShoot)
        {
            Shoot();
        }
    }

    public Vector2 GetClosestEnemy(Vector2 objPos)
    {
        return enemy.transform.position;
    }

    private void Shoot()
    {

    }

    private void ResetShooting()
    {
        canShoot = false;
        useTimer = 0.0f;
    }

    
}
