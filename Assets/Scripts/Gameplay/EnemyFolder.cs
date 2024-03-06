using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFolder : MonoBehaviour
{
    public bool HasChildren;
    
    private GameObject[] objectsInRange;
    
    void Update()
    {
        HasChildren = transform.childCount <= 0;
    }

    public GameObject GetEnemiesInRange(Vector2 objPos, float range)
    {
        Enemy[] enemies = GetEnemies();
        if (enemies.Length == 0)
            return null;
        else if (enemies.Length == 1)
            return enemies[0].gameObject;

        GameObject closestObject = enemies[0].gameObject;
        float closestDistance = range;
        for (int i = 0; i < enemies.Length; i++)
        {
            float dist = GetAbsDistance(objPos, (Vector2)enemies[i].gameObject.transform.position);
            if (dist < closestDistance)
            {
                closestObject = enemies[i].gameObject;
                closestDistance = dist;
            }
        }
        if (objectsInRange.Length == 0)
        {
            return null;
        }
        else
        {
            return closestObject;
        }
    }

    public Vector2 GetClosestEnemy(Vector2 pos)
    {
        Enemy[] enemies = GetEnemies();
        if (enemies.Length == 0)
            return Vector2.zero;
        else if (enemies.Length == 1)
            return enemies[0].gameObject.transform.position;

        GameObject obj = null;
        float currentdist = GetAbsDistance(pos, enemies[0].transform.position);
        for (int i = 1; i < enemies.Length; i++)
        {
            float dist = GetAbsDistance(pos, enemies[i].gameObject.transform.position);
            if (dist <= currentdist)
            {
                currentdist = dist;
                obj = enemies[i].gameObject;
            }
        }

        if (obj != null)
            return obj.transform.position;
        else
            return Vector2.zero;
    }

    public Vector2 GetClosestEnemy(Vector2 pos, float range)
    {
        Enemy[] enemies = GetEnemies();
        if (enemies.Length == 0)
            return Vector2.zero;
        else if (enemies.Length == 1)
            return enemies[0].gameObject.transform.position;

        GameObject obj = null;
        float currentdist = GetAbsDistance(pos, enemies[0].transform.position);
        for (int i = 1; i < enemies.Length; i++)
        {
            float dist = GetAbsDistance(pos, enemies[i].gameObject.transform.position);
            if (dist <= currentdist && dist <= range)
            {
                currentdist = dist;
                obj = enemies[i].gameObject;
            }
        }

        if (obj != null)
            return obj.transform.position;
        else
            return Vector2.zero;
    }

    public Enemy[] GetEnemies()
    {
        return GetComponentsInChildren<Enemy>();
    }

    public float GetAbsDistance(Vector2 distFrom, Vector2 distTo)
    {
        return Mathf.Abs(Vector2.Distance(distFrom, distTo));
    }
}
