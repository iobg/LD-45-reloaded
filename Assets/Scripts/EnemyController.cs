using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour


{
	public float moveSpeed = 0.5f;
    public float maxHealth = 100;

    Vector3 aimDirection;
    float currentHealth;
    // bool KOed = false;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void Damage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        // KO if no health left
        if (currentHealth <= 0){
            Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
            rigidbody2d.constraints = RigidbodyConstraints2D.None;
            rigidbody2d.mass = 0.2f;
            rigidbody2d.drag = 3.0f;
            rigidbody2d.AddTorque(20); // ********** Death torque ********
            Vector2 knockbackDirection = new Vector2(-aimDirection.x, -aimDirection.y); knockbackDirection.Normalize();
            rigidbody2d.AddForce(knockbackDirection * 50); // ******** Death velocity ********
            Destroy(gameObject);
            // KOed = true;
        }
    }
}
