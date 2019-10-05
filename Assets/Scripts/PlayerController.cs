using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
