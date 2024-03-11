using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingAI : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyScript;

    Vector2 currentTower, attack;
    GameObject tryTowerObj;
    private bool isAttacking = false;

    private void Start()
    {
        SetRandomAttack();
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
                else Attack();
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
    private void SetRandomAttack()
    {
        float randNum = UnityEngine.Random.Range(1.5f, 3.0f);
        attack = new(randNum, randNum);
    }

    private void Attack()
    {
        float distance = enemyScript.GetAbsDistance(currentTower + attack);
        if (distance <= .1f)
        {
            FlipAttack();
        }
        else
        {
            enemyScript.MoveTowardsLocation(currentTower + attack);
        }
    }

    private void FlipAttack()
    {
        int randAttack = UnityEngine.Random.Range(0, 5);
        switch (randAttack)
        {
            case 0:
                attack *= new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2));
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                attack = -attack;
                break;
        }

    }
}
