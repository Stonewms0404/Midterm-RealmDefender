using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action<GameObject, Vector2> _SpawnParticles;

    [SerializeField]
    private ProjectileScriptableObject projectileSO;
    [SerializeField]
    private Rigidbody2D rb;

    private SpawnTowers spawnTowers;
    private EnemyFolder enemyFolder;
    private GameObject selectedObject;
    private float timer;

    private void Start()
    {
        spawnTowers = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<SpawnTowers>();

        if (projectileSO.projType == ProjectileScriptableObject.ProjectileType.TOWERHEAL)
            selectedObject = spawnTowers.GetClosestTower(transform.position);
        else if (projectileSO.projType == ProjectileScriptableObject.ProjectileType.ENEMYHEAL)
            selectedObject = enemyFolder.GetClosestEnemy(transform.position);
    }

    void Update()
    {
        switch (projectileSO.projType)
        {
            case ProjectileScriptableObject.ProjectileType.DAMAGETOWER:
                DamageTower();
                break;
            case ProjectileScriptableObject.ProjectileType.DAMAGEENEMY:
                DamageEnemy();
                break;
            case ProjectileScriptableObject.ProjectileType.ENEMYHEAL:
                HealEnemy();
                break;
            case ProjectileScriptableObject.ProjectileType.TOWERHEAL:
                HealTower();
                break;
        }

        if (timer < projectileSO.lifetime)
            timer += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void HealTower()
    {
        if (selectedObject != null)
        {
            Move();
        }
    }
    private void HealEnemy()
    {

    }
    private void DamageEnemy()
    {
        selectedObject = spawnTowers.GetClosestTower(transform.position);
        if (selectedObject != null)
        {
            Move();
        }
    }
    private void Move()
    {

    }

    private void DamageTower()
    {
        selectedObject = spawnTowers.GetClosestTower(transform.position);
    }


    public int GetUseAmount()
    {
        return projectileSO.useAmount;
    }
    public ProjectileScriptableObject.ProjectileType GetProjectileType()
    {
        return projectileSO.projType;
    }

    private void OnDestroy()
    {
        _SpawnParticles(projectileSO.particles, gameObject.transform.position);
    }
}
