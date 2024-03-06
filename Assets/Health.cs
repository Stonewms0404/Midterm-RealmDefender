using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action GameOver;

    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private int health;

    private void Start()
    {
        AdjustHealth(-maxHealth);
    }

    private void OnEnable()
    {
        FinishBox.EnemyFinished += AdjustHealth;
    }
    private void OnDisable()
    {
        FinishBox.EnemyFinished -= AdjustHealth;
    }

    public void AdjustHealth(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
            GameOver();
        SetHealthText();
    }

    private void SetHealthText()
    {
        healthText.text = "Health " + health;
    }
}
