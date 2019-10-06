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
        if (Input.GetKeyDown("1")){
            equippedObject = "fists";
            equippedSprite.sprite = null;
        } else if (Input.GetKeyDown("2")){
            equippedObject = "pickaxe";
            equippedSprite.sprite = spriteArray[0];
        } else if (Input.GetKeyDown("3")){
            equippedObject = "scythe";
            equippedSprite.sprite = spriteArray[1];
        }

        // On player click
        if (Input.GetMouseButton(0) && actionTimer <= 0)
        {
            actionTimer = actionSpeed;
            Vector3Int affectedTile = tilemap.getAffectedTile();
            // Depending on what is equipped
            switch(equippedObject)
            {
                case "fists":
                    punchTile(affectedTile);
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
        }


        //Reduce timers
        if (actionTimer > 0){
            actionTimer -= Time.deltaTime;
        }
    }



    // If player has fists
    void punchTile(Vector3Int affectedTile){
        return;
    }


    // If player has pickaxe
    void pickTile(Vector3Int affectedTile){
        tilemap.destroyTile(affectedTile);
        return;
    }

    // If player has scythe
    void harvestTile(Vector3Int affectedTile){
        return;
    }

}
