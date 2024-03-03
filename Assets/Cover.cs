using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public static event Action<bool> Hovered;
    public bool hovered = false;

    public void MouseEnter()
    {
        hovered = true;
        Hovered(false);
    }
}
