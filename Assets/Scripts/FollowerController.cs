using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{	
	public float moveSpeed;
    private float vertical;
    private float horizontal;
    public Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   vertical = PlayerController.instance.vertical -1 ;
        horizontal = PlayerController.instance.horizontal-1;
        Vector2 move = new Vector2(horizontal, vertical);
        position = position + move * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(move);
    }
}
