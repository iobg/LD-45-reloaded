using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public static InventoryController instance {get; private set;}

    public int PistolCats;
    public int RifleCats;
    public int SniperCats;
    public int PistolSeeds;
    public int RifleSeeds;
    public int SniperSeeds;
    public int Key;

    public bool pickaxe = false;
    public bool scythe = false;

    private GameObject inventoryHUD;

    void Awake()
    {
        // This object can persist between scenes
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}