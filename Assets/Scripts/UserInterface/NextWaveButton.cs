using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    public static event Action NextWaveButtonClicked;

    [SerializeField]
    private GameObject nextWaveButton;

    public void NextWave()
    {
        nextWaveButton.SetActive(false);
        NextWaveButtonClicked();
    }

    public void WaveCompleted(int wave)
    {
        nextWaveButton.SetActive(true);
    }


}
