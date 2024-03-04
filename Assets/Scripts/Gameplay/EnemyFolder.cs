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
        {
            return null;
        }

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

    public Enemy[] GetEnemies()
    {
        return GetComponentsInChildren<Enemy>();
    }

    public float GetAbsDistance(Vector2 distFrom, Vector2 distTo)
    {
        return Mathf.Abs(Vector2.Distance(distFrom, distTo));
    }
}
