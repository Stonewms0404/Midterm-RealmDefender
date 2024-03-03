using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static event Action<int, GameObject> _BuyTower;
    public static event Action<Transform, GameObject> _OpenTowerMenu;
    public static event Action _GotHit;

    [SerializeField] private TowerScriptableObject towerSO;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject towerObject;

    private int health;

    public bool shopOpen = false;

    private void Start()
    {
        health = towerSO.health;
        if (buttonText)
        {
            buttonText.text = "\n\n\n\nCost\n" + towerSO.costOfTower + "G";
        }
    }

    public int GetCostOfTower()
    {
        return towerSO.costOfTower;
    }

    public int GetCurrentHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return towerSO.health;
    }

    public void Hit(int amount)
    {
        health -= amount;
        _GotHit();
    }


    public void BuyTower()
    {
        _BuyTower(towerSO.costOfTower, towerObject);
    }

    private void OnMouseDown()
    {
        if (!buttonText && !shopOpen)
        {
            tag = "TowerSelected";
            _OpenTowerMenu(this.gameObject.transform, this.gameObject);
        }
    }
}
