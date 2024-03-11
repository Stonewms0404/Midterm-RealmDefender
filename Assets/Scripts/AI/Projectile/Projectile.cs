using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Timeline;

public class Projectile : MonoBehaviour
{
    public static event Action<GameObject, Vector2> _SpawnParticles;
    public static event Action<GameObject, Vector2> _SpawnSFX;

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
                selectedObject = spawnTowers.GetClosestTower(this.transform.position).transform.position;
                break;
            case ProjectileScriptableObject.ProjectileType.DAMAGEENEMY:
                selectedObject = enemyFolder.GetClosestEnemy(this.transform.position, projectileSO.sightRange);
                break;
            case ProjectileScriptableObject.ProjectileType.TOWERHEAL:
                Tower tempTower;
                do
                {
                    tempTower = spawnTowers.GetRandomTower(this.transform.position);
                    if (tempTower == null)
                        continue;
                    else
                        selectedObject = tempTower.gameObject.transform.position;
                } while (selectedObject == (Vector2)this.transform.position && tempTower.GetTowerType() != TowerScriptableObject.TowerType.Cleric);
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
        {
            foundObject = true;
            _SpawnSFX(projectileSO.SFX, transform.position);
        }

        Vector2 direction = selectedObject - (Vector2)transform.position;

        rb.velocity = projectileSO.speed * direction.normalized;
    }

    void Update()
    {
        
        if (timer < projectileSO.lifetime)
            timer += Time.deltaTime;
        else
            Destroy(this.gameObject);

        if (rb.position == selectedObject &&
            projectileSO.projType == ProjectileScriptableObject.ProjectileType.TOWERHEAL ||
            projectileSO.projType == ProjectileScriptableObject.ProjectileType.ENEMYHEAL)
            Destroy(gameObject);
    }

    public int GetUseAmount()
    {
        return projectileSO.useAmount;
    }
    public ProjectileScriptableObject.SpecialEffect GetSpecialEffect()
    {
        return projectileSO.specialEffect;
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
