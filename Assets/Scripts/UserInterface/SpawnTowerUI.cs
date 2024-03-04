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
    private Wallet wallet;

    [SerializeField] private bool canShow = true;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        SelectedTile._DisplayShop += DisplayShop;
        Tower._BuyTower += BuyTower;
        ToggleShop(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Mathf.Abs(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition))) > 10.0f)
            {
                ToggleShop(false);
            }
        }
            
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

    public void UpdateButtons(int wallet)
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
        if (costOfTower <= wallet.GetWallet())
        {
            _Transaction(costOfTower);
            _PlaceTower(towerObject, spawnPosition.transform);
            ToggleShop(false);
        }
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
