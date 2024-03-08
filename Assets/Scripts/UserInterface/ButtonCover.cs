using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCover : MonoBehaviour
{
    public bool hovered;

    private void OnMouseEnter()
    {
        hovered = true;
        Debug.Log(hovered);
    }
    private void OnMouseExit()
    {
        hovered = false;
        Debug.Log(hovered);
    }
}
