using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectedUI : MonoBehaviour
{
    public static event Action<bool> _TowerMenuOpen;
    public static event Action<int> _SellTower;

    [SerializeField]
    private GameObject towerSelectedUI, selectedTowerObj;
    [SerializeField]
    public TextMeshProUGUI healthText, sellText;

    private Tower selectedTower;
    private bool isOpen = false;
    private int sellAmount;
    private bool towerSold;

    private void Start()
    {
        towerSelectedUI.SetActive(false);
        Tower._OpenTowerMenu += OpenMenu;
        Tower._GotHit += AdjustTowerHealthText;
    }

    private void ToggleMenu()
    {
        if (isOpen == false && !towerSold)
        {
            GameObject.FindGameObjectWithTag("TowerSelected").gameObject.tag = "Untagged";
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

        AdjustTowerHealthText();
        sellAmount = (int)((float)selectedTower.GetCostOfTower() * (selectedTower.GetCurrentHealth() / selectedTower.GetMaxHealth()));
        sellText.text = sellAmount + "G";
        towerSold = false;

        towerSelectedUI.transform.position = trans.position;
        isOpen = true;
        ToggleMenu();
    }

    private void AdjustTowerHealthText()
    {
        healthText.text = selectedTower.GetCurrentHealth() + "/" + selectedTower.GetMaxHealth() + " HP";
    }

    public void SellTower()
    {
        Debug.Log("selectedTower: " + selectedTower.name);
        Destroy(GameObject.FindGameObjectWithTag("TowerSelected"));
        _SellTower(sellAmount);
        towerSold = true;
        CloseMenu();
    }

    private void OnMouseExit()
    {
        isOpen = false;
        ToggleMenu();
    }
}
