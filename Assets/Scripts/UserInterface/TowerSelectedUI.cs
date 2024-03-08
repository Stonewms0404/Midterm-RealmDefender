using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectedUI : MonoBehaviour
{
    public static event Action<bool> _TowerMenuOpen;
    public static event Action<int> _SellTower;

    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private GameObject towerSelectedUI, selectedTowerObj;
    [SerializeField]
    private Button sellTowerButton;

    private Tower selectedTower;
    private bool isOpen = false, towerSold = false;
    private int sellAmount;

    public TextMeshProUGUI healthText, sellText;
    private object boundsAdjusted;

    private void Start()
    {
        towerSelectedUI.SetActive(false);
        Tower._OpenTowerMenu += OpenMenu;
    }

    private void OnEnable()
    {
        Tower._GotHit += AdjustTowerHealthText;
    }

    private void OnDisable()
    {
        Tower._GotHit -= AdjustTowerHealthText;
    }

    private void Update()
    {
        if (pauseMenu.isPaused)
        {
            CloseMenu();
        }
        else if (isOpen && !pauseMenu.isPaused)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDist = new(MathF.Abs(mousePos.x - transform.position.x), MathF.Abs(mousePos.y - transform.position.y));
            if (Input.GetMouseButtonDown(1))
            {
                CloseMenu();
            }
            else if (mouseDist.x >= 8.0f || mouseDist.y >= 4.0f)
            {
                CloseMenu();
            }
            else if (selectedTower.IsDestroyed())
            {
                CloseMenu();
            }
            //Vector2 dist = new(GetAbsDistance(transform.position.x, mousePos.x), GetAbsDistance(transform.position.y, mousePos.y));
            //if (dist.x > boundsAdjusted.x || dist.y > boundsAdjusted.y)
            //{
            //    isOpen = false;
            //    ToggleMenu();
            //}
        }
    }

    private void ToggleMenu()
    {
        if (isOpen == false && !towerSold)
        {
            GameObject.FindGameObjectWithTag("TowerSelected").tag = "Untagged";
        }
        towerSelectedUI.SetActive(!towerSelectedUI.activeSelf);
        selectedTower.shopOpen = isOpen;
        _TowerMenuOpen(!isOpen);
    }

    public void CloseMenu()
    {
        isOpen = false;
        ToggleMenu();
        selectedTower = null;
    }

    private void OpenMenu(Transform trans, GameObject currentTower)
    {
        selectedTower = currentTower.GetComponent<Tower>();

        AdjustTowerHealthText(selectedTower.GetCurrentHealth());
        towerSold = false;
        sellAmount = (int)(selectedTower.GetCostOfTower() * ((float)selectedTower.GetCurrentHealth() / (float)selectedTower.GetMaxHealth()));
        sellText.text = sellAmount + "G";

        towerSelectedUI.transform.position = trans.position;
        isOpen = true;
        ToggleMenu();
    }

    private void AdjustTowerHealthText(int newHealth)
    {
        if (newHealth <= 0)
        {
            CloseMenu();
        }
        healthText.text = newHealth + "/" + selectedTower.GetMaxHealth() + " HP";
        AdjustTowerSellValue(newHealth);
    }

    private void AdjustTowerSellValue(int newHealth)
    {
        sellAmount = (int)(selectedTower.GetCostOfTower() * ((float)newHealth / (float)selectedTower.GetMaxHealth()));
        sellText.text = sellAmount + "G";
    }


    public void SellTower()
    {
        Destroy(GameObject.FindGameObjectWithTag("TowerSelected"));
        _SellTower(sellAmount);
        towerSold = true;
        CloseMenu();
    }
}
