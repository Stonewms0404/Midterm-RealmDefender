using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
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
    private Vector2 selectedObject;
    private float timer;
    public bool canHit = true;

    private void Start()
    {
        spawnTowers = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<SpawnTowers>();
        enemyFolder = GameObject.FindGameObjectWithTag("EnemyFolder").GetComponent<EnemyFolder>();

        if (projectileSO.projType == ProjectileScriptableObject.ProjectileType.DAMAGETOWER)
            selectedObject = spawnTowers.GetClosestTower(transform.position);
        else if (projectileSO.projType == ProjectileScriptableObject.ProjectileType.ENEMYHEAL ||
            projectileSO.projType == ProjectileScriptableObject.ProjectileType.DAMAGEENEMY)
            selectedObject = enemyFolder.GetClosestEnemy(transform.position);
        else if (projectileSO.projType == ProjectileScriptableObject.ProjectileType.TOWERHEAL)
        {
            do
            {
                selectedObject = spawnTowers.GetRandomTower(transform.position);
            } while (selectedObject == (Vector2)transform.position);
        }
    }

    void Update()
    {
        rb.position = Vector2.MoveTowards(rb.position, selectedObject, projectileSO.speed * Time.deltaTime);
        
        if (timer < projectileSO.lifetime)
            timer += Time.deltaTime;
        else
            Destroy(this.gameObject);

        if (rb.position == selectedObject)
            canHit = false;

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
        if (projectileSO.particles)
            _SpawnParticles(projectileSO.particles, gameObject.transform.position);
    }
}
