using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private Tilemap tilemap;

    private GameObject selectedTower;
    private Vector3Int mousePos;
    private Tile mouseTile;

    private void Update()
    {
        if (Input.mousePresent)
        {
            Vector3 mouse = Input.mousePosition;
            mousePos = new((int)mouse.x, (int)mouse.y, 1);
        }
        mouseTile = (Tile)tilemap.GetTile(mousePos);

        if (Input.GetAxis("Fire1") != 0)
        {
            SpawnTower();
        }
    }

    private void SpawnTower()
    {
        Instantiate(selectedTower, mousePos, Quaternion.identity);
    }
}
