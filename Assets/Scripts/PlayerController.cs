using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PistolCat;
    public GameObject RifleCat;
    public GameObject SniperCat;
    public float maxHealth;

    private int globalY;
    private int globalX;
    private char lastUpdate = 'x';
    private float currentHealth;



    // Singleton
    public static PlayerController instance {get; private set;}

    // Public variables
	public float moveSpeed;

    // Private variables

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        globalX = 1;
        globalY = 1;
        createCats();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = GetComponent<Rigidbody2D>().position;
        position = position + move * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(position);

        if(horizontal > 0){
             GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(horizontal < 0){
              GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    Vector2 getNextOffset(Vector2 offset){
        if (offset.x == globalX && lastUpdate == 'y' ){
            globalX++;
            offset.x = globalX *-1;
            lastUpdate = 'x';
        }
         else if (offset.y == globalY && lastUpdate == 'x'){
            globalY++;
            offset.y = globalY *-1;
            lastUpdate = 'y';
        }
        else if (offset.x == offset.y && lastUpdate == 'x' && offset.y < globalY){
            offset.y++;
            lastUpdate = 'y';
        }
        else if (offset.x == offset.y && lastUpdate == 'y' && offset.x < globalX){
            offset.x++;
            lastUpdate= 'x';
        }
        else if (offset.y < offset.x && lastUpdate == 'x'){
            offset.y++;
            lastUpdate= 'y';
        }
        else if (offset.x < offset.y && lastUpdate == 'y'){
            offset.x++;
            lastUpdate= 'x';
        }
        if(offset.x == 0 && offset.y == 0){
            offset.x = 1;
            offset.y = 1;
        }
        return offset;

    }


    void createCats(){
        Vector2 currentOffset = new Vector2(-1, -1);
        while (InventoryController.instance.PistolCats > 0){
            FollowerController newCat =  Instantiate(PistolCat, GetComponent<Rigidbody2D>().position, Quaternion.Euler(0f, 0f, 0f)).GetComponent<FollowerController>();
            currentOffset = getNextOffset(currentOffset);
            newCat.initialize(currentOffset);
            InventoryController.instance.PistolCats--;
        }
        while (InventoryController.instance.SniperCats > 0){
            FollowerController newCat =  Instantiate(SniperCat, GetComponent<Rigidbody2D>().position, Quaternion.Euler(0f, 0f, 0f)).GetComponent<FollowerController>();
            currentOffset = getNextOffset(currentOffset);
            newCat.initialize(currentOffset);
             InventoryController.instance.SniperCats--;
        }
        while (InventoryController.instance.RifleCats > 0){
            FollowerController newCat =  Instantiate(RifleCat, GetComponent<Rigidbody2D>().position, Quaternion.Euler(0f, 0f, 0f)).GetComponent<FollowerController>();
            currentOffset = getNextOffset(currentOffset);
            newCat.initialize(currentOffset);
            InventoryController.instance.RifleCats--;
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        // KO if no health left
        if (currentHealth <= 0){
            Debug.Log("GAME OVER");
            // do lots of other stuff here
        }
    }
}
