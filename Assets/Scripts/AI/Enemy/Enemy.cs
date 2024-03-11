using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<int> RewardMoney;
    public static event Action<GameObject, Vector2> _SpawnParticles;

    [SerializeField] EnemyScriptableObject enemySO;
    [SerializeField] GameObject frozenSprite;
    [SerializeField] SpriteRenderer sprite;

    private int health, poisonDamage = 5;
    private float speed, maxHealth, effectTimer, effectTimerToReach = 1.5f, poisonTimer, poisonTimerToReach = 5.0f, DOTTimer, DOTTimerToReach = 0.4f, effectSpeed;
    private Vector3 locatorSelected;
    private SpawnTowers spawnTowers;
    private GameObject[] enemyLocator;
    private WavesManager wavesManager;

    public bool touchingTower, towerInRange, isFrozen, isPoisoned;
    public Rigidbody2D rb;

    private void Start()
    {
        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();

        maxHealth = enemySO.health * wavesManager.GetDifficultyMultiplier();
        health = (int)maxHealth;

        speed = enemySO.speed * wavesManager.GetDifficultyMultiplier();

        spawnTowers = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<SpawnTowers>();
        enemyLocator = GameObject.FindGameObjectsWithTag("EnemyLocator");
        locatorSelected = enemyLocator[UnityEngine.Random.Range(1, enemyLocator.Length)].transform.position;
        EnemyUnfrozen();
    }

    private void FixedUpdate()
    {
        if (isPoisoned)
        {
            EnemyPoisoned();
            sprite.color = Color.green;
            DamageOverTime();
        }

        if (isFrozen)
        {
            EnemyFrozen();
            frozenSprite.SetActive(isFrozen);
        }
    }

    private void DamageOverTime()
    {
        if (DOTTimer < DOTTimerToReach)
            DOTTimer += Time.deltaTime;
        else
        {
            DOTTimer = 0.0f;
            _SpawnParticles(enemySO.poisonParticles, transform.position);
            Hit(poisonDamage);
        }
    }

    private void EnemyFrozen()
    {
        isFrozen = effectTimer < effectTimerToReach;
        if (isFrozen)
        {
            effectTimer += Time.deltaTime;
            effectSpeed = 0.1f;
        }
        else
        {
            EnemyUnfrozen();
        }
    }
    private void EnemyUnfrozen()
    {
        effectTimer = 0;
        effectSpeed = 1.0f;
        frozenSprite.SetActive(isFrozen);
    }

    private void EnemyPoisoned()
    {
        isPoisoned = poisonTimer < poisonTimerToReach;
        if (isPoisoned)
        {
            poisonTimer += Time.deltaTime;
            effectSpeed = 0.8f;
        }
        else
        {
            EnemyUnpoisoned();
        }
    }
    private void EnemyUnpoisoned()
    {
        poisonTimer = 0.0f;
        effectSpeed = 1.0f;
        sprite.color = Color.white;
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
    public GameObject FindNextTower(Vector2 pos)
    {
        GameObject currentTower = spawnTowers.GetClosestTower(pos);
        return currentTower;
    }

    public void MoveTowardsLocator()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, locatorSelected, effectSpeed * speed * Time.deltaTime);
    }

    public void MoveTowardsLocation(Vector2 locationPos)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, locationPos, effectSpeed * speed * Time.deltaTime);
    }
    public void MoveTowardsLocation(Vector2 locationPos, float adjustedSpeed)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, locationPos, adjustedSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        collision.TryGetComponent<Tower>(out Tower temp);
        if (temp != null && enemySO.enemyType == EnemyScriptableObject.EnemyType.EXPOLODER)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            collision.gameObject.TryGetComponent<Projectile>(out Projectile collProj);
            collision.gameObject.TryGetComponent<KnightsSword>(out KnightsSword sword);
            if (collProj)
            {
                ObjectHit(collProj, collision.gameObject);
            }
            else if (sword)
            {
                Hit(sword.GetUseAmount());
            }
        }
    }

    private void ObjectHit(Projectile proj, GameObject obj)
    {
        switch (proj.GetProjectileType())
        {
            case ProjectileScriptableObject.ProjectileType.DAMAGEENEMY:
                Hit(proj.GetUseAmount());
                
                switch (proj.GetSpecialEffect())
                {
                    case ProjectileScriptableObject.SpecialEffect.NORMAL:
                        Destroy(obj);
                        break;
                    case ProjectileScriptableObject.SpecialEffect.FREEZE:
                        EnemyFrozen();
                        Destroy(obj);
                        break;
                    case ProjectileScriptableObject.SpecialEffect.POISON:
                        EnemyPoisoned();
                        Destroy(obj);
                        break;
                }
                break;
            case ProjectileScriptableObject.ProjectileType.ENEMYHEAL:
                _SpawnParticles(enemySO.healParticels, transform.position);
                Hit(-proj.GetUseAmount());
                break;
        }
    }

    public void Hit(int amount)
    {
        health -= amount;
        health = Math.Clamp(health, 0, (int)maxHealth);
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
        if (enemySO.deathParticles)
            _SpawnParticles(enemySO.deathParticles, transform.position);
    }

    internal int GetSpeed()
    {
        return enemySO.speed;
    }
}
