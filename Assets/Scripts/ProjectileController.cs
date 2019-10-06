using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

	Rigidbody2D rigidbody2d;


	public float bulletSpeed = 300.0f; // Note that speed is affected by mass too
    public float bulletMass = 0.3f;
    public float bulletStun = 0.3f;
    
    float bulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
    	rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.mass = bulletMass;
    }


    public void Launch(Vector2 direction, float bulletDamage)
    {
        direction.Normalize();
        rigidbody2d.AddForce(direction * bulletSpeed);
        this.bulletDamage = bulletDamage;
    }

        void OnTriggerEnter2D(Collider2D col)
    {	

        if(col.name == "Walls"){
        	 Destroy(gameObject);
        }
        if(col.tag == "Enemy" && gameObject.tag == "friendlyBullet"){
      		Destroy(gameObject);

      		 EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null) {
            	enemy.Stun((float)0.5);
                enemy.Damage(bulletDamage);
            }
        }

        if(col.tag == "Follower" && gameObject.tag == "enemyBullet"){
      		Destroy(gameObject);

        }

         if(col.tag == "Player" && gameObject.tag == "enemyBullet"){
      		Destroy(gameObject);

        }
       
        // restart = true;
        // timer = 0.0f;
    }
}
