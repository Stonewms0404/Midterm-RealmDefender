using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapHovered : MonoBehaviour
{
    public bool hovered;
    private void OnMouseEnter()
    {
        hovered = true;
    }
    private void OnMouseExit()
    {
        hovered = false;
    }
}
