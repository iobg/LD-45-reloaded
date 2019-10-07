using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmerController : MonoBehaviour
{

    // Singleton
    public static FarmerController instance {get; private set;}

    // Public variables
    public float moveSpeed = 10;
    public float actionSpeed = 0.5f;
    public Sprite[] spriteArray; // nothing for fists, [0] for pickaxe, [1] for scythe

    // Private variables
    private string equippedObject;
    private SpriteRenderer equippedSprite;
    private float actionTimer;
    private static TilemapController tilemap;
    private Vector3Int punchedTile; // Helper variable for determining if you've punched a tile twice
    private bool hasPickaxe = false; // Player has obtained pickaxe
    private bool hasScythe = false; // Player has obtained scythe


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Equipped Objects
        equippedObject = "fists";
        equippedSprite = transform.Find("EquippedItem").gameObject.GetComponent<SpriteRenderer>();
        equippedSprite.sprite = null;
        tilemap = TilemapController.instance;
    }

    void Update()
    {
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = GetComponent<Rigidbody2D>().position;
        position = position + move * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(position);

        // Switching equipment
        // Key press 1
        if (Input.GetKeyDown("1")){
            equippedObject = "fists";
            equippedSprite.sprite = null;
        } 
        // Key press 2
        else if (Input.GetKeyDown("2")){
            equippedObject = "pickaxe";
            equippedSprite.sprite = spriteArray[0];
        } 
        // Key press 3
        else if (Input.GetKeyDown("3")){
            equippedObject = "scythe";
            equippedSprite.sprite = spriteArray[1];
        } 
        // Key press 4
        else if (Input.GetKeyDown("4")){
            equippedObject = "seed1";
            equippedSprite.sprite = null;
        }
        // Key press 5
        else if (Input.GetKeyDown("5")){
            equippedObject = "seed2";
            equippedSprite.sprite = null;
        }


        // On player click
        if (Input.GetMouseButton(0) && actionTimer <= 0)
        {
            actionTimer = actionSpeed;
            Vector3Int affectedTile = tilemap.getAffectedCoordinate();
            // Depending on what is equipped
            switch(equippedObject)
            {
                case "fists":
                    if (punchedTile == affectedTile){
                        punchTile(affectedTile);
                    }
                    break;
                case "pickaxe":
                    pickTile(affectedTile);
                    break;
                case "scythe":
                    harvestTile(affectedTile);
                    break;
                default:
                    break;
            }
            // For punching the same tile twice in a row
            punchedTile = affectedTile;
        }


        //Reduce timers
        if (actionTimer > 0){
            actionTimer -= Time.deltaTime;
        }
    }

    // Player has obtained pickaxe/scthe
    public void obtainPickaxe(){
        hasPickaxe = true;
    }
    public void obtainScythe(){
        hasScythe = true;
    }


    // If player has fists
    void punchTile(Vector3Int affectedTile){
        tilemap.punchTile(affectedTile);
        return;
    }


    // If player has pickaxe
    void pickTile(Vector3Int affectedTile){
        tilemap.tillTile(affectedTile);
        return;
    }

    // If player has scythe
    void harvestTile(Vector3Int affectedTile){
        return;
    }

}
