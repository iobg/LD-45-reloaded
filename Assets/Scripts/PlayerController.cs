using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance {get; private set;}
	public float moveSpeed;
    public Vector2 position;
    public float x;
    public float y;


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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        position = GetComponent<Rigidbody2D>().position;
        position = position + move * moveSpeed * Time.deltaTime;
        y = position.y;
        x = position.x;
        GetComponent<Rigidbody2D>().MovePosition(position);
    }
}
