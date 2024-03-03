using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapHovered : MonoBehaviour
{
    public static event Action<string> Hover;
    private void OnMouseEnter()
    {
        Hover(gameObject.name);
    }
    private void OnMouseExit()
    {
        Hover(gameObject.name);
    }
}
