using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAI : MonoBehaviour
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
        towerScript.ShootProjectile(transform, Random.Range(0,2));
    }

    private void ResetShooting()
    {
        useTimer = 0.0f;
        canShoot = false;
    }
}
