using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
    [SerializeField]
    private Tower towerScript;

    private EnemyFolder enemyFolder;
    private float useTimer;
    private bool canAttack = true;

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
                if (useTimer <= towerScript.GetUseTime() && !canAttack)
                    useTimer += Time.deltaTime;
                else
                {
                    canAttack = true;
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            towerScript.ShootProjectile(transform, 0);
        }
        ResetAttack();

    }

    private void ResetAttack()
    {
        useTimer = 0.0f;
        canAttack = false;
    }
}
