using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileController : MonoBehaviour
{

	Rigidbody2D rigidbody2d;

	public ParticleSystem particles;

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
        	 Instantiate(particles, transform.position, transform.rotation);
        }
        if(col.tag == "Enemy" && gameObject.tag == "friendlyBullet"){
      		Destroy(gameObject);
      		Instantiate(particles, transform.position, transform.rotation);
      		 EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null) {
            	enemy.Stun((float)0.5);
                enemy.Damage(bulletDamage);
            }
        }

        if(col.tag == "Follower" && gameObject.tag == "enemyBullet"){
      		Destroy(gameObject);
      		Instantiate(particles, transform.position, transform.rotation);
      		FollowerController follower = col.GetComponent<FollowerController>();
            if (follower != null) {
                follower.Damage(bulletDamage);

            }

        }

         if(col.tag == "Player" && gameObject.tag == "enemyBullet"){
      		Destroy(gameObject);
      		Instantiate(particles, transform.position, transform.rotation);
      		PlayerController.instance.Damage(bulletDamage);
      		GameObject.Find("playerHP").GetComponent<Text>().text = "HP: " + PlayerController.instance.getHealth().ToString() + "/" + PlayerController.instance.maxHealth.ToString();

        }
       
        // restart = true;
        // timer = 0.0f;
    }
}
