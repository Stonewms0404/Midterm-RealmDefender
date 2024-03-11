using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrutionsMenu : MonoBehaviour
{
    [SerializeField] GameObject[] MenuObjs;

    private void OnEnable()
    {
        ToShopMenu();
    }

    public void ToShopMenu()
    {
        int index = 0;
        for (int i = 0; i < MenuObjs.Length; i++)
        {
            if (i == index)
                MenuObjs[i].SetActive(true);
            else
                MenuObjs[i].SetActive(false);
        }
    }
    public void ToTowersMenu()
    {
        int index = 1;
        for (int i = 0; i < MenuObjs.Length; i++)
        {
            if (i == index)
                MenuObjs[i].SetActive(true);
            else
                MenuObjs[i].SetActive(false);
        }
    }
    public void ToEnemiesMenu()
    {
        int index = 2;
        for (int i = 0; i < MenuObjs.Length; i++)
        {
            if (i == index)
                MenuObjs[i].SetActive(true);
            else
                MenuObjs[i].SetActive(false);
        }
    }
}
