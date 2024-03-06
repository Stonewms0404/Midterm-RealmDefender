using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private void OnEnable()
    {
        Tower._ShootProjectile += SpawnProjectile;
        Health.GameOver += DestroyAllProjectiles;
    }
    private void OnDisable()
    {
        Tower._ShootProjectile -= SpawnProjectile;
        Health.GameOver -= DestroyAllProjectiles;
    }

    private void DestroyAllProjectiles()
    {
        Projectile[] projs = GetProjectiles();
        if (projs.Length == 0)
            return;
        for (int i = 0; i < projs.Length; i++)
        {
            Destroy(projs[i].gameObject);
        }
    }

    public Projectile[] GetProjectiles() //Used for getting the child components that have the "Projectile" component.
    {
        return GetComponentsInChildren<Projectile>();
    }

    private void SpawnProjectile(GameObject projectile, Transform trans)
    {
        Instantiate(projectile, trans.position, Quaternion.identity, transform);
    }
}
