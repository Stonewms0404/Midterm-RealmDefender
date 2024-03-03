using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour
{
    [SerializeField]
    private int locatorNum;

    public int GetLocatorNum()
    {
        return locatorNum;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
