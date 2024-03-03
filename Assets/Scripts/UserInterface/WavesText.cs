using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI wavesText;
    private int waves = 0;

    private void Start()
    {
        gameObject.SetActive(false);
        NextWaveButton.NextWaveButtonClicked += ChangeWavesText;
    }

    private void ChangeWavesText()
    {
        ++waves;
        gameObject.SetActive(true);
        wavesText.text = "Wave: " + waves + "/10";
    }
}
