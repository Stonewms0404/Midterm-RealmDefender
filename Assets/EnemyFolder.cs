using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFolder : MonoBehaviour
{
    public bool HasChildren;
    void Update()
    {
        HasChildren = transform.childCount <= 0;
    }
}
