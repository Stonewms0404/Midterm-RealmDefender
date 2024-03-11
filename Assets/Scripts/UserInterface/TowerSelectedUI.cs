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

    [Header ("Objects")]
    [SerializeField] Button sellTowerButton;
    public TextMeshProUGUI healthText, sellText;
    [SerializeField] GameObject towerSelectedUI, selectedTowerObj;
    [SerializeField] GameObject[] menuObj;

    private Tower selectedTower;
    private bool isOpen = false, towerSold = false, menusOpen;
    private int sellAmount;


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
    private void OnDestroy()
    {
        Tower._OpenTowerMenu -= OpenMenu;
    }

    private void SetMenuOpen()
    {
        for (int i = 0; i < menuObj.Length; i++)
        {
            menusOpen = menuObj[i].activeInHierarchy;
            if (menusOpen)
                return;
        }
    }

    private void Update()
    {
        SetMenuOpen();
        if (menusOpen)
        {
            CloseMenu();
        }
        else if (isOpen && !menusOpen)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDist = new(MathF.Abs(mousePos.x - transform.position.x), MathF.Abs(mousePos.y - transform.position.y));
            if (Input.GetMouseButtonDown(1))
            {
                CloseMenu();
            }
            else if (selectedTower.IsDestroyed())
            {
                CloseMenu();
            }
            else if (mouseDist.x >= 8.0f || mouseDist.y >= 4.0f)
            {
                CloseMenu();
            }
        }
    }

    private void ToggleMenu()
    {
        if (isOpen == false && !towerSold)
        {
            GameObject.FindGameObjectWithTag("TowerSelected").tag = "Untagged";
        }
        towerSelectedUI.SetActive(isOpen);
        selectedTower.shopOpen = isOpen;
    }

    public void CloseMenu()
    {
        isOpen = false;
        _TowerMenuOpen(false);
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
        _TowerMenuOpen(true);
        ToggleMenu();
    }

    private void AdjustTowerHealthText(int newHealth)
    {
        if (newHealth <= 0)
        {
            CloseMenu();
        }
        else
        {
            healthText.text = newHealth + "/" + selectedTower.GetMaxHealth() + " HP";
            AdjustTowerSellValue(newHealth);
        }
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

    public bool GetIsOpen()
    {
        return isOpen;
    }    
}
