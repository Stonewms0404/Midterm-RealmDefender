using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExploderAI : MonoBehaviour
{
    [SerializeField]
    Enemy enemyScript;

    GameObject tryTowerObj;
    Vector2 currentTower;
    bool isAttacking;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            if (enemyScript.GetAbsDistance(currentTower) > 5.0f)
            {
                enemyScript.MoveTowardsLocation(currentTower);
            }
            else
            {
                if (tryTowerObj.IsDestroyed()) isAttacking = false;
                else enemyScript.MoveTowardsLocation(currentTower);
            }
        }
        else
        {
            tryTowerObj = enemyScript.FindNextTower();
            if (tryTowerObj != null) // If the tower was found, set the enemy into attacking mode
            {
                currentTower = tryTowerObj.transform.position;
                isAttacking = true;
            }
            else // If the tower was not found, set the enemy into passive moving towards the goal
            {
                isAttacking = false;
                enemyScript.MoveTowardsLocator();
            }
        }
    }
}
