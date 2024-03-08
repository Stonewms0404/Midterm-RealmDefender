using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextWaveButton : MonoBehaviour
{
    public static event Action NextWaveButtonClicked;

    [SerializeField]
    private GameObject nextWaveButton;

    public bool hovered;

    public void NextWave()
    {
        nextWaveButton.SetActive(false);
        NextWaveButtonClicked();
        hovered = false;
    }

    public void WaveCompleted(int wave)
    {
        nextWaveButton.SetActive(true);
    }

    private void OnMouseEnter()
    {
        hovered = true;
    }
    private void OnMouseExit()
    {
        hovered = false;
    }
}
