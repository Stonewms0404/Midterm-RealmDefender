using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static event Action<int> UpdateWallet;
    [SerializeField]
    private TextMeshProUGUI walletText;
    [SerializeField]
    private int startingAmount;

    private int wallet;

    private void Start()
    {
        SpawnTowerUI._Transaction += Transation;
        TowerSelectedUI._SellTower += AddToWallet;
        Enemy.RewardMoney += AddToWallet;
        WavesManager._WaveReward += AddToWallet;

        AddToWallet(startingAmount);
    }

    private void AddToWallet(int amount)
    {
        wallet += amount;
        UpdateWalletText();
        UpdateWallet(wallet);
    }
    private void Transation(int amount)
    {
        wallet -= amount;
        UpdateWalletText();
        UpdateWallet(wallet);
    }

    private void UpdateWalletText()
    {
        walletText.text = wallet + "G";
    }
}
