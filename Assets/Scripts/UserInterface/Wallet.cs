using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI walletText;
    [SerializeField]
    private int startingAmount;
    [SerializeField]
    private SpawnTowerUI spawnTowerUI;

    private int wallet;

    private void Awake()
    {
        SpawnTowerUI._Transaction += Transation;
        TowerSelectedUI._SellTower += AddToWallet;
        Enemy.RewardMoney += AddToWallet;
        WavesManager._WaveReward += AddToWallet;
    }
    private void OnDestroy()
    {
        SpawnTowerUI._Transaction -= Transation;
        TowerSelectedUI._SellTower -= AddToWallet;
        Enemy.RewardMoney -= AddToWallet;
        WavesManager._WaveReward -= AddToWallet;
    }

    public void SetStartingAmount(int amount)
    {
        AddToWallet(amount);
    }

    public int GetWallet()
    {
        return wallet;
    }

    private void AddToWallet(int amount)
    {
        wallet += amount;
        UpdateWalletText();
        spawnTowerUI.UpdateButtons(wallet);
    }
    private void Transation(int amount)
    {
        wallet -= amount;
        UpdateWalletText();
        spawnTowerUI.UpdateButtons(wallet);
    }

    private void UpdateWalletText()
    {
        walletText.text = wallet + "G";
    }
}
