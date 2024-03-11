using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSelecter : MonoBehaviour
{
    [SerializeField] GameObject[] backgroundImages;
    private void Awake()
    {
        SetRandomBackground();
    }
    public void SetRandomBackground()
    {   
        int randNum = UnityEngine.Random.Range(0, backgroundImages.Length);
        GameObject selectedImage = backgroundImages[randNum];
        foreach (GameObject image in backgroundImages)
        {
            if (image == selectedImage)
                image.SetActive(true);
            else
                image.SetActive(false);
        }

    }
}
