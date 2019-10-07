using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{	

    // Public variables
    [SerializeField]
    public Color colorToTurnTo = Color.white;
    public GameObject follower;
	public float moveSpeed = 10f;
    public Vector2 followerOffset;
    public Camera mainCamera;
    public float maxHealth = 1;

    float fireReloadTime = 0;
    public float fireDelay = 0.15f;
    public float bulletDamage = 1.0f;
    public GameObject bulletPrefab;

    // Private variables
    private Vector2 playerPosition;
    private NavMeshAgent agent;
    Vector3 mouseDirection;
    private float currentHealth;
    private Renderer rend;



    // Start is called before the first frame update
    void Start()
    {

        //Navmesh code to lock rotation
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentHealth = maxHealth;


        // Assign Renderer component to rend variable
        rend = GetComponent<Renderer>();

        // Change sprite color to selected color
        rend.material.color = colorToTurnTo;
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(follower.transform.position);

        mouseDirection = Input.mousePosition - new Vector3(screenPosition.x, screenPosition.y, 0);
        playerPosition = PlayerController.instance.GetComponent<Rigidbody2D>().position;
        Vector3 followerPosition = follower.transform.position;

        Vector3 placeToBe = playerPosition + followerOffset;

        // Navmesh move to place
        agent.SetDestination(placeToBe);

        //sprite flip for direction they're moving
        if(placeToBe.x > followerPosition.x){
             GetComponent<SpriteRenderer>().flipX = false;
        }
        else {
              GetComponent<SpriteRenderer>().flipX = true;
        }

        if (fireReloadTime > 0){
            fireReloadTime -= Time.deltaTime;
            return;
        } 

         if (Input.GetMouseButton(0))
        {
            Launch();
        }

    }


        public void Launch()
    {
        // If the cooldown is good
        if (fireReloadTime <= 0)
        {

            // Get player position
            Vector3 followerPosition = follower.transform.position;
            

            // fireReloadTime = fireDelay;
            float atan2 = Mathf.Atan2(mouseDirection.y, mouseDirection.x);
            GameObject bullet = Instantiate(bulletPrefab, followerPosition, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg));
            ProjectileController projectile = bullet.GetComponent<ProjectileController>();
            projectile.Launch(mouseDirection, bulletDamage);
            fireReloadTime += fireDelay;

            // audioSource.PlayOneShot(shootClip);
        }
    
    }


     public void Damage(float damage)
    {
        currentHealth -= damage;
        // KO if no health left
        if (currentHealth <= 0){
            Destroy(gameObject);
        }
    }

    public void initialize(Vector2 offset){
        followerOffset = offset;
    }
}


