using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action EnemyDeath;
    public static event Action<int> RewardMoney;

    [SerializeField]
    private EnemyScriptableObject enemySO;
    [SerializeField]
    private Rigidbody2D rb;

    private WavesManager wavesManager;
    private Vector2 locatorSelected;

    private int health;


    private void Start()
    {
        health = enemySO.health;
        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();
        locatorSelected = wavesManager.GetEnemyLocator();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, 2 * locatorSelected, enemySO.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Projectile"))
        //{
        //    Hit(1);
        //}
    }

    public void Hit(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            RewardMoney(enemySO.moneyReward);
            Death();
        }
    }

    public void Death()
    {
        //EnemyDeath();
        Destroy(this.gameObject);
    }
}
