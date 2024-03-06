using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static event Action<int, GameObject> _BuyTower;
    public static event Action<Transform, GameObject> _OpenTowerMenu;
    public static event Action<int> _GotHit;
    public static event Action<GameObject, Vector2> _Death;
    public static event Action<GameObject, Transform> _ShootProjectile;

    [SerializeField] private TowerScriptableObject towerSO;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject towerObject;

    private int health;
    private WavesManager wavesManager;

    public bool shopOpen = false, isDead = false;

    private void Start()
    {
        health = towerSO.health;

        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();

        if (buttonText)
        {
            buttonText.text = "\n\n\n\nCost\n" + towerSO.costOfTower + "G";
        }
    }

    public bool GetIfWaveIsRunning()
    {
        return wavesManager.GetHasEnemiesLeft();
    }

    public void ShootProjectile(Transform trans, int projIndex)
    {
        _ShootProjectile(towerSO.projectile[projIndex], trans);
    }
    public float GetSightRange()
    {
        return towerSO.sightRange;
    }
    public float GetUseTime()
    {
        return towerSO.useSpeed;
    }
    public int GetCostOfTower()
    {
        return towerSO.costOfTower;
    }
    public int GetCurrentHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return towerSO.health;
    }

    public void Hit(int amount)
    {
        health -= amount;
        health = Math.Clamp(health, 0, towerSO.health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (CompareTag("TowerSelected"))
        {
            _GotHit(health);
        }
    }

    public void BuyTower()
    {
        _BuyTower(towerSO.costOfTower, towerObject);
    }

    //Showing collisions to see what to do with said collision
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy collidedEnemy = collision.gameObject.GetComponent<Enemy>();
            Hit(collidedEnemy.GetAttack());
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile collProj = collision.gameObject.GetComponent<Projectile>();
            if (collProj.canHit)
            {
                switch (collProj.GetProjectileType())
                {
                    case ProjectileScriptableObject.ProjectileType.DAMAGETOWER:
                        Hit(collProj.GetUseAmount());
                        Destroy(collision.gameObject);
                        break;
                    case ProjectileScriptableObject.ProjectileType.TOWERHEAL:
                        if (towerSO.towerType != TowerScriptableObject.TowerType.Cleric)
                        {
                            Hit(-collProj.GetUseAmount());
                            Destroy(collision.gameObject);
                        }
                        break;
                }
            }
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

    private void OnMouseDown()
    {
        if (!buttonText && !shopOpen)
        {
            tag = "TowerSelected";
            _OpenTowerMenu(this.gameObject.transform, this.gameObject);
        }
    }

    //When the tower gets destroyed.
    private void OnDestroy()
    {
        if (!buttonText && towerSO.deathParticles)
            _Death(towerSO.deathParticles, transform.position);
    }
}
