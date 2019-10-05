using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{	

    // Public variables
    public GameObject follower;
	public float moveSpeed = 10f;
    public Vector2 followerOffset;
    public Camera mainCamera;

    float fireReloadTime = 0;
    public float fireDelay = 0.15f;
    public float bulletDamage = 1.0f;
    public GameObject bulletPrefab;

    // Private variables
    private Vector2 playerPosition;
    private NavMeshAgent agent;
    Vector3 mouseDirection;



    // Start is called before the first frame update
    void Start()
    {
        //Navmesh code to lock rotation
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(follower.transform.position);

        mouseDirection = Input.mousePosition - new Vector3(screenPosition.x, screenPosition.y, 0);
        playerPosition = PlayerController.instance.GetComponent<Rigidbody2D>().position;
        Vector2 placeToBe = playerPosition + followerOffset;

        // Navmesh move to place
        agent.SetDestination(placeToBe);

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
            // Cost ammo
            // HealthBar.instance.changeValue(-ammoValue);

            // Get player position
            Vector3 playerPosition = follower.transform.position;

            // fireReloadTime = fireDelay;
            float atan2 = Mathf.Atan2(mouseDirection.y, mouseDirection.x);
            GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg));
            ProjectileController projectile = bullet.GetComponent<ProjectileController>();
            projectile.Launch(mouseDirection, bulletDamage);
            fireReloadTime += fireDelay;

            // audioSource.PlayOneShot(shootClip);
        }
    
    }
}
