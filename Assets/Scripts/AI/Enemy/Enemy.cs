using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<int> RewardMoney;
    public static event Action<GameObject, Vector2> _Death;

    [SerializeField]
    private EnemyScriptableObject enemySO;

    private int health;
    private Vector3 locatorSelected;
    private SpawnTowers spawnTowers;
    private GameObject[] enemyLocator;

    public bool touchingTower, towerInRange;
    public Rigidbody2D rb;

    private void Start()
    {
        health = enemySO.health;

        spawnTowers = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<SpawnTowers>();
        enemyLocator = GameObject.FindGameObjectsWithTag("EnemyLocator");
        locatorSelected = enemyLocator[UnityEngine.Random.Range(1, enemyLocator.Length)].transform.position;
    }

    public int GetAttack()
    {
        return enemySO.attack;
    }
    public GameObject FindNextTower()
    {
        GameObject currentTower = spawnTowers.GetRandomTower();
        return currentTower;
    }

    public void MoveTowardsLocator()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, locatorSelected, enemySO.speed * Time.deltaTime);
    }

    public void MoveTowardsLocation(Vector2 locationPos)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, locationPos, enemySO.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        collision.TryGetComponent<Tower>(out Tower temp);
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Hit(1);
        }
        if (temp != null && enemySO.enemyType == EnemyScriptableObject.EnemyType.EXPOLODER)
        {
            Destroy(gameObject);
        }

    }

    public void Hit(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            RewardMoney(enemySO.moneyReward);
            Destroy(this.gameObject);
        }
    }

    public float GetAbsDistance(Vector2 obj)
    {
        return Mathf.Abs(Vector2.Distance((Vector2)transform.position, obj));
    }
    public float GetAbsDistance(Vector2 current, Vector2 obj)
    {
        return Mathf.Abs(Vector2.Distance(current, obj));
    }

    private void OnDestroy()
    {
        _Death(enemySO.deathParticles, transform.position);
    }
}
