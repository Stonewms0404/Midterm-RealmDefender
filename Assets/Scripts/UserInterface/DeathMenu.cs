using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] string[] deathMessage;
    [SerializeField] GameObject deathMenuUIobj;
    [SerializeField] TextMeshProUGUI wavesSurvivedText, deathMessageText;

    private WavesManager wavesManager;

    private void Start()
    {
        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();
        CloseMenu();
    }

    private void OnEnable()
    {
        Health.GameOver += GameOver;
    }
    private void OnDisable()
    {
        Health.GameOver -= GameOver;
    }

    private void GameOver()
    {
        OpenMenu();
        int wavesSurvived = wavesManager.GetWaves() - 1;

        wavesSurvivedText.text = "Waves Survived: " + wavesSurvived + "/10!";
        deathMessageText.text = deathMessage[UnityEngine.Random.Range(0, deathMessage.Length)];
    }

    private void OpenMenu()
    {
        deathMenuUIobj.SetActive(true);
    }

    private void CloseMenu()
    {
        deathMenuUIobj.SetActive(false);
    }
}
