using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance {get; private set;}
	public float moveSpeed;
    public Vector2 position;
    public float horizontal;
    public float vertical;

    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        position = GetComponent<Rigidbody2D>().position;
        position = position + move * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(position);
    }
}
