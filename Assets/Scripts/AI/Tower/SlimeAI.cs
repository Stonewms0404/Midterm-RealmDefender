using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private Tower towerScript;

    private EnemyFolder enemyFolder;
    private float useTimer;
    private bool canShoot = true;
    private int enemyNum;

    private void Start()
    {
        enemyFolder = GameObject.FindGameObjectWithTag("EnemyFolder").GetComponent<EnemyFolder>();
    }

    private void Update()
    {
        if (towerScript.GetIfWaveIsRunning())
        {
            enemyNum = enemyFolder.GetNumEnemiesInRange(this.transform.position, towerScript.GetSightRange());
            if (enemyNum > 0)
            {
                towerScript.LookTowardsObject(enemyFolder.GetClosestEnemy(this.transform.position, towerScript.GetSightRange()));
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
        for (int i = 0; i < enemyNum; i++)
            towerScript.ShootProjectile(transform, 0);
    }

    private void ResetShooting()
    {
        useTimer = 0.0f;
        canShoot = false;
    }
}
