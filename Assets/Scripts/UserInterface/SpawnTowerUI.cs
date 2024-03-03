using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnTowerUI : MonoBehaviour
{
    public static event Action<int> _Transaction;
    public static event Action<GameObject, Transform> _PlaceTower;
    public static event Action<bool> _ShopOpen;

    [SerializeField]
    private GameObject spawnTowerUI, spawnPosition;
    [SerializeField]
    private UnityEngine.UI.Button[] towerButtons;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private int wallet;

    [SerializeField] private bool canShow = true;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        SelectedTile._DisplayShop += DisplayShop;
        Wallet.UpdateWallet += SetWallet;
        Tower._BuyTower += BuyTower;
        ToggleShop(false);
    }

    public void DisplayShop(string input, Transform trans)
    {
        if (input == "Show" && canShow)
        {
            spawnTowerUI.transform.position = trans.position + offset;

            ToggleShop(true);
        }
        if (input == "Hide")
        {
            ToggleShop(false);
        }
    }

    private void ToggleShop(bool value)
    {
        spawnTowerUI.SetActive(value);
        _ShopOpen(value);
    }

    private void SetWallet(int newAmount)
    {
        wallet = newAmount;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < towerButtons.Length; i++) 
        {
            Tower towerComponent = towerButtons[i].GetComponent<Tower>();
            if (towerComponent.GetCostOfTower() <= wallet)
            {
                towerButtons[i].image.color = new(1, 1, 1, 1);
                towerButtons[i].interactable = true;
            }
            else if (towerComponent.GetCostOfTower() > wallet)
            {
                towerButtons[i].image.color = new(1, .5f, .5f, .75f);
                towerButtons[i].interactable = false;
            }
        }
    }

    private void BuyTower(int costOfTower, GameObject towerObject)
    {
        if (costOfTower <= wallet)
        {
            _Transaction(costOfTower);
            _PlaceTower(towerObject, spawnPosition.transform);
            ToggleShop(false);
        }
    }

    private void SetCanShow()
    {
        ToggleShop(false);
    }

    private void OnMouseEnter()
    {
        _ShopOpen(true);
    }

    private void OnMouseExit()
    {
        ToggleShop(false);
    }
}
