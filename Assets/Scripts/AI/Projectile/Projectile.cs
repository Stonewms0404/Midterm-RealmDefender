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
    public bool canHit = true, foundObject;

    private void Start()
    {
        spawnTowers = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<SpawnTowers>();
        enemyFolder = GameObject.FindGameObjectWithTag("EnemyFolder").GetComponent<EnemyFolder>();

        switch (projectileSO.projType)
        {
            case ProjectileScriptableObject.ProjectileType.DAMAGETOWER:
                selectedObject = spawnTowers.GetClosestTower(this.transform.position);
                break;
            case ProjectileScriptableObject.ProjectileType.DAMAGEENEMY:
                selectedObject = enemyFolder.GetClosestEnemy(this.transform.position, projectileSO.sightRange);
                break;
            case ProjectileScriptableObject.ProjectileType.TOWERHEAL:
                do
                {
                    selectedObject = spawnTowers.GetRandomTower(this.transform.position);
                } while (selectedObject == (Vector2)this.transform.position);
                break;
            case ProjectileScriptableObject.ProjectileType.ENEMYHEAL:
                break;
        }

        if (selectedObject == Vector2.zero)
        {
            foundObject = false;
            Destroy(gameObject);
        }
        else
            foundObject = true;
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
        if (projectileSO.particles && foundObject)
            _SpawnParticles(projectileSO.particles, gameObject.transform.position);
    }
}
