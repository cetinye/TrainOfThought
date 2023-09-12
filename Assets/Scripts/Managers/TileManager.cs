using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap tileMap;
    public GameObject tile;

    public Vector3Int location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        // Clone object on mouse click at the mouse position
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            location = tileMap.WorldToCell(mp);
            Instantiate(tile, location, Quaternion.identity);
        }
    }
}
