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

    [Header ("Interfaces")]
    [SerializeField] Wallet wallet;
    [SerializeField] GameObject[] menuObj;
    [SerializeField] UnityEngine.UI.Button[] towerButtons;

    [Space]
    [Header ("Objects")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject spawnTowerUI, canvasObj, circleObj;
    [SerializeField] Camera mainCamera;

    [Space]
    [Header("Variables")]
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 bounds, boundsAdjusted;
    [SerializeField] bool towerMenuOpen, isOpen, menusOpen;

    private void Awake()
    {
        SelectedTile._DisplayShop += DisplayShop;
        TowerSelectedUI._TowerMenuOpen += SetCanShow;
        Tower._BuyTower += BuyTower;
        ToggleShop(false);
    }

    private void OnDestroy()
    {
        SelectedTile._DisplayShop -= DisplayShop;
        TowerSelectedUI._TowerMenuOpen -= SetCanShow;
        Tower._BuyTower -= BuyTower;
    }
    private void SetMenuOpen()
    {
        for (int i = 0; i < menuObj.Length; i++)
        {
            menusOpen = menuObj[i].activeSelf;
            if (menusOpen)
                return;
        }
    }

    private void Update()
    {
        SetMenuOpen();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (menusOpen)
        {
            ToggleShop(false);
        }
        else if (gameObject.activeSelf && !menusOpen)
        {
            Vector2 dist = new(GetAbsDistance(transform.position.x, mousePos.x), GetAbsDistance(transform.position.y, mousePos.y));
            SetCanvasSize();
            if (dist.x > boundsAdjusted.x || dist.y > boundsAdjusted.y)
            {
                ToggleShop(false);
            }
        }
    }

    private void SetCanShow(bool value)
    {
        towerMenuOpen = value;
    }

    public void DisplayShop(string input, Transform trans)
    {
        if (input == "Show" && !towerMenuOpen && !menusOpen)
        {
            spawnTowerUI.transform.position = trans.position + offset;
            SetCanvasSize();

            ToggleShop(true);
        }
        if (input == "Hide")
        {
            ToggleShop(false);
        }
    }

    private void SetCanvasSize()
    {
        canvas.transform.localScale = new(mainCamera.orthographicSize / 10, mainCamera.orthographicSize / 10, mainCamera.orthographicSize / 10);
        boundsAdjusted.x = bounds.x * canvas.transform.localScale.x;
        boundsAdjusted.y = bounds.y * canvas.transform.localScale.y;
    }

    private void ToggleShop(bool value)
    {
        isOpen = value;
        canvasObj.SetActive(value);
        circleObj.SetActive(value);
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
            _PlaceTower(towerObject, circleObj.transform);
            ToggleShop(false);
        }
    }

    private void OnMouseEnter()
    {
        _ShopOpen(true);
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

    private float GetAbsDistance(Vector2 to)
    {
        return Mathf.Abs(Vector2.Distance(transform.position, to));
    }
    private float GetAbsDistance(Vector2 from, Vector2 to)
    {
        return Mathf.Abs(Vector2.Distance(from, to));
    }

    private float GetAbsDistance(float from, float to)
    {
        return Mathf.Abs(from - to);
    }
}