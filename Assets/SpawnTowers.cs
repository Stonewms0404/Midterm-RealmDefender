using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnTowerUI._PlaceTower += SpawnTower;
    }

    private void SpawnTower(GameObject towerObject, Transform trans)
    {
        Instantiate(towerObject, trans.position, Quaternion.identity, gameObject.transform);
    }
}
