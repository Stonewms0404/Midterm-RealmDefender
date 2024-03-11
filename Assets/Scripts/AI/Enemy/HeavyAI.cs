using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyAI : MonoBehaviour
{
    [SerializeField] Enemy enemyScript;
    [SerializeField] float speedMultiplier;

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
            tryTowerObj = enemyScript.FindNextTower(transform.position);
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
            enemyScript.MoveTowardsLocation(currentTower + attack, enemyScript.GetSpeed() * speedMultiplier);
        }
    }

    private void FlipAttack()
    {
        attack = -attack;
    }
}
