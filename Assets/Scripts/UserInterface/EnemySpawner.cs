using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnerNum, waveNum;
    [SerializeField]
    private float waveMultiplier;

    private void Awake()
    {
        WavesManager.SpawnEnemyAtSpawner += SpawnEnemy;
    }
    private void OnDestroy()
    {
        WavesManager.SpawnEnemyAtSpawner -= SpawnEnemy;
    }

    private void SpawnEnemy(int numOfSpawner, GameObject enemy, Transform enemyFolder)
    {
        if (spawnerNum == numOfSpawner)
        {
            Instantiate(enemy, transform.position, Quaternion.identity, enemyFolder);
        }
    }

}
