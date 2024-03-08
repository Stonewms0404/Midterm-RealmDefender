using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFolder : MonoBehaviour
{
    public bool HasChildren;
    
    private GameObject[] objectsInRange;

    public bool GetHasChildren() //Finding if there are enemies left within the gameobject.
    {
        HasChildren = transform.childCount > 0;
        return HasChildren;
    }

    private void OnEnable()
    {
        Health.GameOver += DestroyAllEnemies;
    }
    private void OnDisable()
    {
        Health.GameOver -= DestroyAllEnemies;
    }

    private void DestroyAllEnemies()
    {
        Enemy[] enemies = GetEnemies();
        if (enemies.Length == 0)
            return;
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
    }

    public GameObject[] GetEnemiesInRange(Vector2 objPos, float range) //Unimplemented method for Enemy Healing.
    {
        Enemy[] enemies = GetEnemies();
        GameObject[] enemiesInRange = null;

        if (enemies.Length == 0)
            return null;

        float closestDistance = range;
        for (int i = 0; i < enemies.Length; i++)
        {
            float dist = GetAbsDistance(objPos, (Vector2)enemies[i].gameObject.transform.position);
            if (dist < closestDistance)
            {
                enemiesInRange[enemiesInRange.Length] = enemies[i].gameObject;
                closestDistance = dist;
            }
        }
        if (objectsInRange.Length == 0)
        {
            return null;
        }
        else
        {
            return enemiesInRange;
        }
    }

    public Vector2 GetClosestEnemy(Vector2 pos, float range) //Finding the closest enemy's position within range, if nothing within range, it return (0, 0).
    {
        Enemy[] enemies = GetEnemies();
        if (enemies.Length == 0)
            return Vector2.zero;
        else if (enemies.Length == 1)
        {
            float dist = GetAbsDistance(pos, enemies[0].transform.position);
            if (dist <= range)
                return enemies[0].transform.position;
            else
                return Vector2.zero;
        }

        float currentdist = GetAbsDistance(pos, enemies[0].transform.position);
        GameObject obj = null;
        if (currentdist <= range)
            obj = enemies[0].gameObject;
        for (int i = 0; i < enemies.Length; i++)
        {
            float dist = GetAbsDistance(pos, enemies[i].gameObject.transform.position);
            if (dist <= currentdist)
            {
                if (dist <= range)
                {
                    currentdist = dist;
                    obj = enemies[i].gameObject;
                }
            }
        }

        if (obj != null)
            return obj.transform.position;
        else
            return Vector2.zero;
    }

    public int GetNumEnemiesInRange(Vector2 pos, float range) //Used for Ai to keep it lightweight to find how many enemies there are on scene.
    {
        Enemy[] enemies = GetEnemies();
        int numOfEnemiesInRange = 0;
        if (enemies.Length == 0)
            return numOfEnemiesInRange;
        else
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                float dist = GetAbsDistance(pos, enemies[i].transform.position);
                if (dist <= range)
                {
                    numOfEnemiesInRange++;
                }
            }
        }
        return numOfEnemiesInRange;
    }

    public Enemy[] GetEnemies() //Used for getting the child components that have the "Enemy" component.
    {
        return GetComponentsInChildren<Enemy>();
    }

    public float GetAbsDistance(Vector2 distFrom, Vector2 distTo) //Shorthand for Mathf.Abs(Vector2.Distance(obj1, obj2)).
    {
        return Mathf.Abs(Vector2.Distance(distFrom, distTo));
    }
}
