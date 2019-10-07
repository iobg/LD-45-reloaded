using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleController : MonoBehaviour
{

    // Helper variables for pickaxe and scythe logic
    private float prepareTimer = 1.5f;
    private bool isTool = false;

    // Start is called before the first frame update
    void Start()
    {
        // If it's a pickaxe or a scythe, don't want it to be collected within the first few seconds
        // Also give it some starting velocity
        if (gameObject.tag == "PickaxeCollectible" || gameObject.tag == "ScytheCollectible"){
            GetComponent<BoxCollider2D>().enabled = false;
            isTool = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Tools can't be picked up instantly
        if(isTool && !GetComponent<BoxCollider2D>().enabled){
            // If it hasn't been enough time yet
            if (prepareTimer > 0.0f){
                prepareTimer -= Time.deltaTime;
            }
            // If enough time has passed
            else{
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.name == "Player"){
            if (gameObject.tag == "Key"){
                Destroy(gameObject);
                InventoryController.instance.Key++;
            }
            if (gameObject.tag == "Door" && InventoryController.instance.Key >= 1){
                Destroy(gameObject);
                InventoryController.instance.Key--;
            }

            if (gameObject.tag == "Portal"){
                SceneManager.LoadScene("FarmingScene");
            }
            if (gameObject.tag == "PickaxeCollectible"){
                Destroy(gameObject);
                FarmerController.instance.obtainPickaxe();
            }
            if (gameObject.tag == "ScytheCollectible"){
                Destroy(gameObject);
                FarmerController.instance.obtainScythe();
            }

        }
    }

}