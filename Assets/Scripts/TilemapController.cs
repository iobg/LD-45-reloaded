using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    //Singleton
    public static TilemapController instance {get; private set;}

    //Public variables
    public TileBase hoverTile; // Tile for the hover visual
    public static Vector3Int affectedCoordinate;
    public TileBase[] tileArray; // in order: null grass dirt wall

    //Private variables
    private static Tilemap hoverMap;   //TODO if time: don't use an entire Tilemap for the hover visual
    private static Tilemap groundMap;
    private static Grid grid;
    private TileBase farmerTile;
    private static FarmerController farmer;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hoverMap = GetComponent<Tilemap>();
        grid = hoverMap.layoutGrid;
        groundMap = grid.transform.Find("Tilemap").gameObject.GetComponent<Tilemap>();
        farmer = FarmerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // To make sure there's only one selected tile at a time
        hoverMap.ClearAllTiles();

        /*
        // save the camera as public field if you using not the main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = grid.WorldToCell(worldPoint);
        // Visual indicator of the tile the mouse is hovering
        hoverMap.SetTile(position, hoverTile);
        */

        // Get the tile that we are facing
        affectedCoordinate = getAffectedTile();
        //Debug.Log(affectedCoordinate);
        // Visual indicator of that tile
        hoverMap.SetTile(affectedCoordinate, hoverTile);
    }


//    public Vector3Int getAffectedTile(){
    private Vector3Int getAffectedTile(){
        // What tile is the farmer standing on?
        Vector3Int farmerCoordinate = grid.WorldToCell(farmer.GetComponent<Rigidbody2D>().position);
        farmerTile = groundMap.GetTile(farmerCoordinate);
        // What direction is the mouse relative to the farmer?
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(farmer.GetComponent<Rigidbody2D>().position);
        Vector3 mouseDirection = Input.mousePosition - new Vector3(screenPosition.x, screenPosition.y, 0);
        // let's get the tile in that direction (adjacent to the farmer)
        Vector3Int affectedCoordinate = farmerCoordinate;
        if (Mathf.Abs(mouseDirection.x) > Mathf.Abs(mouseDirection.y)){
            affectedCoordinate.x += (int)Mathf.Sign(mouseDirection.x);
        }else{
            affectedCoordinate.y += (int)Mathf.Sign(mouseDirection.y);
        }
        //Debug.Log(affectedCoordinate);

        return affectedCoordinate;
    }

    public Vector3Int getAffectedCoordinate(){
        return affectedCoordinate;
    }

    // Destroy a tile
    public void destroyTile(Vector3Int tileCoordinate){
        groundMap.SetTile(tileCoordinate, null);
    }

    // Turn a tile into dirt
    public void tillTile(Vector3Int tileCoordinate){
        if (groundMap.GetTile(tileCoordinate).name == "GRASS"){
            groundMap.SetTile(tileCoordinate, findTile("DirtRuleTile"));
        }
    }


    // Helper method for finding the tile by name in tileArray
    private TileBase findTile(string name){
        foreach(TileBase myTile in tileArray){
            
             if(myTile != null && myTile.name == name)
             {
                 return myTile;
             }
        }
        // If not found
        Debug.Log("Tile " + name +" not found in tileArray. See TilemapController.findTile(string)");
        return null;
    }

}
