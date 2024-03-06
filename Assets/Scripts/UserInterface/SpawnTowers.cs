using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowers : MonoBehaviour
{
    private void OnEnable()
    {
        SpawnTowerUI._PlaceTower += SpawnTower;
        Health.GameOver += DestroyAllTowers;
    }
    private void OnDisable()
    {
        SpawnTowerUI._PlaceTower -= SpawnTower;
        Health.GameOver -= DestroyAllTowers;
    }
    private void DestroyAllTowers()
    {
        Tower[] towers = GetTowers();
        if (towers.Length == 0)
            return;
        for (int i = 0; i < towers.Length; i++)
        {
            Destroy(towers[i].gameObject);
        }
    }

    //Spawn a bought tower.
    private void SpawnTower(GameObject towerObject, Transform trans) 
    {
        Instantiate(towerObject, trans.position, Quaternion.identity, gameObject.transform);
    }



    //Find all towers, and get a random one not based upon location.
    public GameObject GetRandomTower() 
    {
        Tower[] towers = GetTowers();
        if (IsTowerArrayEmpty(towers))
            return null;
        else
            return towers[UnityEngine.Random.Range(0, towers.Length)].gameObject;
    }
    //Find all towers, and get a random one based upon location.
    public Vector2 GetRandomTower(Vector2 pos)
    {
        Tower[] towers = GetTowers();
        if (IsTowerArrayEmpty(towers))
            return Vector2.zero;
        else if (towers.Length == 1 && (Vector2)towers[0].transform.position != pos)
            return towers[0].gameObject.transform.position;
        else
        {
            int randNum = UnityEngine.Random.Range(0, towers.Length);
            return towers[randNum].gameObject.transform.position;
        }
    }

    //Find all towers, and check the distance between the position and the towers and find the closest.
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

    //Find the number of towers.
    public int FindNumTowers()
    {
        int numberOfTowersInRange = 0;
        Tower[] towers = GetTowers();

        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i].transform.position != transform.position)
            {
                numberOfTowersInRange++;
            }
        }

        return numberOfTowersInRange;
    }

    //Find all of the towers currently.
    public Tower[] GetTowers()
    {
        return GetComponentsInChildren<Tower>();
    }
    
    //Find if there are towers currently.
    public bool IsTowerArrayEmpty(Tower[] towers)
    {
        return towers.Length == 0;
    }

    //Shorthand for Mathf.Abs(Vector2.Distance(obj1, obj2)).
    public float GetAbsDistance(Vector2 distFrom, Vector2 distTo)
    {
        return Mathf.Abs(Vector2.Distance(distFrom, distTo));
    }
}
