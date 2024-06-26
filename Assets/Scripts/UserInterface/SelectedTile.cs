using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectedTile : MonoBehaviour
{
    public static event Action<string, Transform> _DisplayShop;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 offset;
    [SerializeField] private NextWaveButton waveButton;
    [SerializeField] private bool isShopOpen, towerMenuOpen;

    private void Awake()
    {
        SpawnTowerUI._ShopOpen += SetIsShopOpen;
        TowerSelectedUI._TowerMenuOpen += SetTowerMenuOpen;
    }

    private void OnDestroy()
    {
        SpawnTowerUI._ShopOpen -= SetIsShopOpen;
        TowerSelectedUI._TowerMenuOpen -= SetTowerMenuOpen;
    }

    private void SetTowerMenuOpen(bool value)
    {
        towerMenuOpen = value;
        isShopOpen = false;
        _DisplayShop("Hide", transform);
    }

    private void Update()
    {
        DisplayShopAtLocation();
        if (!isShopOpen && !towerMenuOpen)
        {
            Vector3 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            transform.position = mouse + offset;
        }
    }

    private void DisplayShopAtLocation()
    {
        if (Input.GetMouseButtonDown(0) && !isShopOpen && !towerMenuOpen && !waveButton.hovered)
        {
            _DisplayShop("Show", transform);
        }
        if (Input.GetMouseButtonDown(1))
        {
            isShopOpen = false;
            _DisplayShop("Hide", transform);
        }
    }

    private void SetIsShopOpen(bool value)
    {
        isShopOpen = value;
    }

    /*
    private void ItemsHovered(string itemHovered)
    {
        //Saving a possible instance of a repeated item.
        int possibleIndex = -1;

        //Looping through hovered items to find a possible matching item.
        for (int i = 0; i < hoveredItem.Length; i++) 
        {
            //If a match happens, it will save the index and stop looping.
            if (hoveredItem[i] == itemHovered) 
            {
                possibleIndex = i;
                break;
            }
        }

         // If the possibleIndex value is still -1,
         // No item was found, and the item can be saved.

        if (possibleIndex == -1)
        {
            hoveredItem[hoveredItem.Length] = itemHovered;
        }
        
         // If the possibleIndex is anything but -1,
         // It loops through starting at the possibleIndex,
         // And adjusts the array by removing the instance.

        else
        {
            for (int i = possibleIndex; i < hoveredItem.Length - 1; i++)
            {
                hoveredItem[i] = hoveredItem[i + 1];
            }
        }
    }
        */

}
