using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    public static event Action<int> EnemyFinished;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Enemy>(out Enemy temp);
        if (temp != null)
        {
            EnemyFinished(temp.GetAttack());
            Destroy(collision.gameObject);
        }
    }
}
