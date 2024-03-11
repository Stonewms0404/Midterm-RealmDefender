using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] string[] victoryMessage;
    [SerializeField] GameObject victoryMenuUIobj;
    [SerializeField] TextMeshProUGUI congratsText, victoryMessageText;

    private WavesManager wavesManager;

    private void Start()
    {
        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();
        CloseMenu();
    }

    private void OnEnable()
    {
        WavesManager._GameFinished += GameFinished;
    }
    private void OnDisable()
    {
        WavesManager._GameFinished -= GameFinished;
    }

    private void GameFinished()
    {
        OpenMenu();
        string level = FormatDifficultyLevel();

        congratsText.text = "Congrats! You finished " + FormatDifficultyLevel() + " difficulty!";
        victoryMessageText.text = victoryMessage[UnityEngine.Random.Range(0, victoryMessage.Length)];
    }

    private string FormatDifficultyLevel()
    {
        return wavesManager.GetDifficulty() switch
        {
            WavesManager.DifficultyLevels.EASY => "Easy",
            WavesManager.DifficultyLevels.NORMAL => "Normal",
            WavesManager.DifficultyLevels.HARD => "Hard",
            _ => "",
        };
    }

    private void OpenMenu()
    {
        victoryMenuUIobj.SetActive(true);
    }

    private void CloseMenu()
    {
        victoryMenuUIobj.SetActive(false);
    }
}
