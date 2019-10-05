using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = GetComponent<Rigidbody2D>().position;
        position = position + move * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(position);
    }
}
