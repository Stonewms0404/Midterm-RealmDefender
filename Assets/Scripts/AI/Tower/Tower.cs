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
    public static event Action<GameObject, Vector2> _SpawnParticle;
    public static event Action<GameObject, Transform> _ShootProjectile;

    [SerializeField] TowerScriptableObject towerSO;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject towerObject;
    [SerializeField] WavesManager wavesManager;
    [SerializeField] SpawnTowerUI shopUI;

    private int health;

    public bool shopOpen = false, isDead = false, towerMenuOpen = false;

    private void Awake()
    {
        SpawnTowerUI._ShopOpen += SetShopOpen;
        TowerSelectedUI._TowerMenuOpen += SetTowerMenuOpen;
    }

    private void Start()
    {
        health = towerSO.health;
        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();
        GameObject.FindGameObjectWithTag("Shop").TryGetComponent<SpawnTowerUI>(out SpawnTowerUI spawnTower);

        if (spawnTower)
            shopUI = spawnTower;

        if (buttonText)
            buttonText.text = "\n\n\n\nCost\n" + towerSO.costOfTower + "G";
    }
    private void Update()
    {
        if (isDead)
            Destroy(gameObject);
    }

    private void SetTowerMenuOpen(bool value)
    {
        towerMenuOpen = value;
    }

    public void LookTowardsObject(Vector2 obj)
    {
        if (transform.position.x - Mathf.Abs(obj.x) <= 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public TowerScriptableObject.TowerType GetTowerType()
    {
        return towerSO.towerType;
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
    private void SetShopOpen(bool value)
    {
        shopOpen = value;
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
            isDead = true;
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
            collision.gameObject.TryGetComponent<Projectile>(out Projectile collProj);// Knight's Sword does not have the projectile component.
            if (collProj && towerSO.towerType != TowerScriptableObject.TowerType.Cleric)
            {
                switch (collProj.GetProjectileType())
                {
                    case ProjectileScriptableObject.ProjectileType.DAMAGETOWER:
                        Hit(collProj.GetUseAmount());
                        Destroy(collision.gameObject);
                        break;
                    case ProjectileScriptableObject.ProjectileType.TOWERHEAL:
                        Hit(-collProj.GetUseAmount());
                        _SpawnParticle(towerSO.healParticels, transform.position);
                        Destroy(collision.gameObject);
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
        if (!buttonText && !shopOpen && !shopUI.GetIsOpen() && !towerMenuOpen)
        {
            tag = "TowerSelected";
            _OpenTowerMenu(this.gameObject.transform, this.gameObject);
        }
    }

    //When the tower gets destroyed.
    private void OnDestroy()
    {
        if (!buttonText && towerSO.deathParticles)
            _SpawnParticle(towerSO.deathParticles, transform.position);
        SpawnTowerUI._ShopOpen -= SetShopOpen;
        TowerSelectedUI._TowerMenuOpen -= SetTowerMenuOpen;
    }
}
