using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    	}
    }

}
