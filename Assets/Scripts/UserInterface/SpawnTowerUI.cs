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
    private PauseMenu pauseMenu;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject spawnTowerUI, spawnPosition;
    [SerializeField]
    private UnityEngine.UI.Button[] towerButtons;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Wallet wallet;

    [SerializeField] private bool canShow = true;
    [SerializeField] private Vector3 offset, bounds, boundsAdjusted;

    private void Start()
    {
        SelectedTile._DisplayShop += DisplayShop;
        Tower._BuyTower += BuyTower;
        ToggleShop(false);
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pauseMenu.isPaused)
        {
            ToggleShop(false);
        }
        else if (gameObject.activeSelf && !pauseMenu.isPaused)
        {
            Vector2 dist = new(GetAbsDistance(transform.position.x, mousePos.x), GetAbsDistance(transform.position.y, mousePos.y));
            SetCanvasSize();
            if (dist.x > boundsAdjusted.x || dist.y > boundsAdjusted.y)
            {
                ToggleShop(false);
            }
        }
    }

    public void DisplayShop(string input, Transform trans)
    {
        if (input == "Show" && canShow && !pauseMenu.isPaused)
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