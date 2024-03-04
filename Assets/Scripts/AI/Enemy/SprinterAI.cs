using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterAI : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyScript;

    private void Update()
    {
        enemyScript.MoveTowardsLocator();
    }
}
