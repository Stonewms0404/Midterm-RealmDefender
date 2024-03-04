using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowers : MonoBehaviour
{
    void Start()
    {
        SpawnTowerUI._PlaceTower += SpawnTower;
    }

    private void SpawnTower(GameObject towerObject, Transform trans)
    {
        Instantiate(towerObject, trans.position, Quaternion.identity, gameObject.transform);
    }

    public GameObject GetRandomTower()
    {
        Tower[] towers = GetTowers();
        if (IsTowerArrayEmpty(towers))
            return null;
        else
            return towers[UnityEngine.Random.Range(0, towers.Length)].gameObject;
    }

    public Tower[] GetTowers()
    {
        return GetComponentsInChildren<Tower>();
    }
    
    public bool IsTowerArrayEmpty(Tower[] towers)
    {
        return towers.Length == 0;
    }
}
