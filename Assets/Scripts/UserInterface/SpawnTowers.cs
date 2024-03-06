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

    public Vector2 GetClosestTower(Vector2 pos)
    {
        Tower[] towers = GetTowers();
        if (IsTowerArrayEmpty(towers))
            return Vector2.zero;
        else if (towers.Length == 1)
            return towers[0].gameObject.transform.position;

        GameObject obj = towers[0].gameObject;
        for (int i = 1; i < towers.Length; i++)
        {
            if (GetAbsDistance(obj.transform.position, towers[i].gameObject.transform.position) <= GetAbsDistance(obj.transform.position, pos))
                obj = towers[i].gameObject;
        }

        return obj.transform.position;
    }

    public Tower[] GetTowers()
    {
        return GetComponentsInChildren<Tower>();
    }
    
    public bool IsTowerArrayEmpty(Tower[] towers)
    {
        return towers.Length == 0;
    }

    public float GetAbsDistance(Vector2 distFrom, Vector2 distTo)
    {
        return Mathf.Abs(Vector2.Distance(distFrom, distTo));
    }
}
