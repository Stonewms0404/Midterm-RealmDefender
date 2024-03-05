using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private void OnEnable()
    {
        Tower._ShootProjectile += SpawnProjectile;
    }
    private void OnDisable()
    {
        Tower._ShootProjectile -= SpawnProjectile;
    }

    private void SpawnProjectile(GameObject projectile, Transform trans)
    {
        Instantiate(projectile, trans.position, Quaternion.identity, transform);
        Destroy(projectile, 5.0f);
    }
}
