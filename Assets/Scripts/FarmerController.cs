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
    public bool hasPickaxe = false; // Player has obtained pickaxe. Public, but has method for changing to true
    public bool hasScythe = false; // Player has obtained scythe. Oublic, but has method for changing to true

    // Private variables
    private string equippedObject;
    private SpriteRenderer equippedSprite;
    private float actionTimer;
    private static TilemapController tilemap;
    private GameObject inventoryCanvas;
    private InventoryController inventory;

    // Helper variable for determining if you've punched a tile twice
    private Vector3Int punchedTile;

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

        // Singletons
        tilemap = TilemapController.instance;
        inventory = InventoryController.instance;

        // Toggle tools off (InvBox2-6, corresponding with pick, scythe, and the three seeds)
        // Warning: this code is actually hilariously bad. 
        // Tell this as a joke at parties if you want ladies to think you're funny
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        string tmpString;
        for (int i = 2; i <= 6; i++){
            switch(i){
                case 2:
                    if(inventory.pickaxe){continue;}
                    break;
                case 3:
                    if(inventory.scythe){continue;}
                    break;
                case 4:
                    if(inventory.PistolSeeds > 0){continue;}
                    break;
                case 5:
                    if(inventory.RifleSeeds > 0){continue;}
                    break;
                case 6:
                    if(inventory.SniperSeeds > 0){continue;}
                    break;
                default:
                    break;
            }
            tmpString = "InvBox" + i.ToString();
            inventoryCanvas.transform.Find(tmpString).transform.Find("Image").gameObject.SetActive(false);
        }
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
        // Direction the sprite faces
        if(horizontal > 0){
             GetComponent<SpriteRenderer>().flipX = false;
             equippedSprite.flipX = false;
        }
        else if (horizontal < 0) {
              GetComponent<SpriteRenderer>().flipX = true;
              equippedSprite.flipX = true;
        }

        // Switching equipment
        // Key press 1
        if (Input.GetKeyDown("1")){
            equippedObject = "fists";
            equippedSprite.sprite = null;
        } 
        // Key press 2
        else if (Input.GetKeyDown("2") && hasPickaxe){
            equippedObject = "pickaxe";
            equippedSprite.sprite = spriteArray[0];
        } 
        // Key press 3
        else if (Input.GetKeyDown("3") && hasScythe){
            equippedObject = "scythe";
            equippedSprite.sprite = spriteArray[1];
        } 
        // Key press 4
        else if (Input.GetKeyDown("4") && inventory.PistolSeeds > 0){
            equippedObject = "pistolSeed";
            equippedSprite.sprite = spriteArray[2];
        }
        // Key press 5
        else if (Input.GetKeyDown("5") && inventory.RifleSeeds > 0){
            equippedObject = "rifleSeed";
            equippedSprite.sprite = spriteArray[3];
        }
        // Key press 6
        else if (Input.GetKeyDown("6") && inventory.SniperSeeds > 0){
            equippedObject = "sniperSeed";
            equippedSprite.sprite = spriteArray[4];
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
                    if (hasPickaxe){
                        pickTile(affectedTile);
                    }
                    break;
                case "scythe":
                    if (hasScythe){
                        harvestTile(affectedTile);
                    }
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
        // I still cringe writing code like this, this would have been a -10 if this were a college assignment
        inventoryCanvas.transform.Find("InvBox2").transform.Find("Image").gameObject.SetActive(true);
        hasPickaxe = true;
    }
    public void obtainScythe(){
        // Make that -20
        inventoryCanvas.transform.Find("InvBox3").transform.Find("Image").gameObject.SetActive(true);
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
