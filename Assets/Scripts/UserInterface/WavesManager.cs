using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public static event Action<int> WaveComplete;
    public static event Action<bool> _WaveStarted;
    public static event Action<int> _WaveReward;
    public static event Action<int, GameObject, Transform> SpawnEnemyAtSpawner;

    [SerializeField]
    private float enemySpawnTimeToReach, waveTimerToReach;
    [SerializeField]
    private int amountOfSpawners;
    [SerializeField]
    private WaveConfigurationScriptableObject[] WaveConfig;
    [SerializeField]
    private GameObject[] Enemies;
    [SerializeField]
    private GameObject enemyFolder;
    [SerializeField]
    private EnemyLocator[] enemyLocator;

    public Vector2 selectedEnemyLocator;

    private EnemyFolder folderOfEnemies;
    private bool canWaveSpawn = false, canSpawnEnemy = false, hasEnemiesLeft, isWaveStarting, isWaveEnding;
    private float enemySpawnTimer = 0, waveMultiplier = 1.0f, waveTimer;
    private int waves = 0;
    private int[] enemyCount = new int[4];


    private void OnEnable()
    {
        folderOfEnemies = enemyFolder.GetComponent<EnemyFolder>();
        NextWaveButton.NextWaveButtonClicked += AddWave;
    }

    public int GetAmountOfSpawners()
    {
        return amountOfSpawners;
    }

    void FixedUpdate()
    {
        if (isWaveStarting)
        {
            if (waveTimer <= waveTimerToReach)
            {
                waveTimer += Time.deltaTime;
            }
            else
            {
                isWaveStarting = false;
                waveTimer = 0.0f;
                _WaveStarted(true);
                canWaveSpawn = true;
            }
        }
        if (canWaveSpawn)
        {
            if (waves > 0)
            {
                SetHasEnemiesLeft();
            }

            if (!canSpawnEnemy)
            {
                if (enemySpawnTimer <= enemySpawnTimeToReach)
                    enemySpawnTimer += Time.deltaTime;
                else
                {
                    canSpawnEnemy = true;
                    enemySpawnTimer = 0.0f;
                }
            }
            if (canSpawnEnemy)
            {
                SpawnEnemy();
            }
            if (!hasEnemiesLeft && folderOfEnemies.HasChildren)
            {
                EndWave();
            }
        }
    }

    public Vector2 GetEnemyLocator()
    {
        int randNum = UnityEngine.Random.Range(0, enemyLocator.Length);
        for (int i = 0; i < enemyLocator.Length; i++)
        {
            if (enemyLocator[i].GetLocatorNum() == randNum)
            {
                selectedEnemyLocator = (Vector2)enemyLocator[i].gameObject.transform.position;
            }
        }
        return selectedEnemyLocator;
    }

    private void SetHasEnemiesLeft()
    {
        hasEnemiesLeft =
            enemyCount[0] != 0 ||
            enemyCount[1] != 0 ||
            enemyCount[2] != 0 ||
            enemyCount[3] != 0;
    }

    private void SpawnEnemy()
    {
        canSpawnEnemy = false;
        SpawnEnemyAtSpawner(UnityEngine.Random.Range(1, 1 + amountOfSpawners), PickEnemy(), enemyFolder.transform);
    }

    private GameObject PickEnemy()
    {
        int randNum;
        while (true)
        {
            randNum = UnityEngine.Random.Range(0, 4);

            if (enemyCount[randNum] == 0)
            {
                continue;
            }
            else
            {
                enemyCount[randNum] -= 1;
                return Enemies[randNum];
            }
        }
    }

    private void AddWave()
    {
        ++waves;
        if (waves == 1)
            waveMultiplier = 1;
        else if (waves >= 1)
            waveMultiplier += 0.5f;
        canSpawnEnemy = true;
        enemySpawnTimeToReach = WaveConfig[waves - 1].spawnTimer;
        enemyCount[0] = WaveConfig[waves - 1].sprint;
        enemyCount[1] = WaveConfig[waves - 1].heavy;
        enemyCount[2] = WaveConfig[waves - 1].flying;
        enemyCount[3] = WaveConfig[waves - 1].exploding;
        isWaveStarting = true;
    }

    private void EndWave()
    {
        canWaveSpawn = true;
        WaveComplete(waves);
        _WaveReward(WaveConfig[waves - 1].moneyReward);
        canWaveSpawn = false;
    }
}
