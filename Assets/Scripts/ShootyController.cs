using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyController : MonoBehaviour
{

	public GameObject crosshair;
	public Camera mainCamera;

	public float fireDelay = 0.15f;
    public float bulletDamage = 1.0f;
    public GameObject bulletPrefab;

    Vector3 mouseDirection;
    float fireReloadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
    	mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    	Vector3 screenPosition = mainCamera.WorldToScreenPoint(crosshair.transform.position);
    	mouseDirection = Input.mousePosition - new Vector3(screenPosition.x, screenPosition.y, 0);


    	 if (fireReloadTime > 0){
            fireReloadTime -= Time.deltaTime;
            return;
        } 
        
    }


    void FixedUpdate()
    {
        // Change position
        Vector2 currentPosition = transform.position;
        Vector2 crosshairPosition; crosshairPosition.x = crosshair.transform.position.x; crosshairPosition.y = crosshair.transform.position.y;
        currentPosition.x = mouseDirection.x / 150.0f;
        currentPosition.y = mouseDirection.y / 150.0f;
        currentPosition = Vector2.ClampMagnitude(currentPosition, 2);
        transform.SetPositionAndRotation(crosshairPosition + currentPosition, Quaternion.Euler(0, 0, 0));
    }


    public void Launch()
    {
        // If the cooldown is good
        if (fireReloadTime <= 0)
        {
            // Cost ammo
            // HealthBar.instance.changeValue(-ammoValue);

            // Get player position
            Vector3 playerPosition = crosshair.transform.position;

            fireReloadTime = fireDelay;
            float atan2 = Mathf.Atan2(mouseDirection.y, mouseDirection.x);
            GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg));
            ProjectileController projectile = bullet.GetComponent<ProjectileController>();
            projectile.Launch(mouseDirection, bulletDamage);

            // audioSource.PlayOneShot(shootClip);
        }
    
    }
}
