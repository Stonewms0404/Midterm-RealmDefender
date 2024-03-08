using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject deathMenuUIobj;
    [SerializeField]
    private TextMeshProUGUI wavesSurvivedText;

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

        if (wavesSurvived == 0)
            wavesSurvivedText.text = "Did you place a tower?";
        else
            wavesSurvivedText.text = "Waves Survived: " + wavesSurvived + "/10!";
    }

    private void OpenMenu()
    {
        deathMenuUIobj.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseMenu()
    {
        deathMenuUIobj.SetActive(false);
        Time.timeScale = 1f;
    }
}
