using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{

    //Public variables

    //Private variables
    private Tilemap map;
    private Grid grid;


    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
        grid = map.layoutGrid;
    }

    // Update is called once per frame
    void Update()
    {

        // save the camera as public field if you using not the main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);

        //Debug.Log(position);
        TileBase hoverTile = map.GetTile(position);
        Debug.Log(hoverTile);
    }
}
