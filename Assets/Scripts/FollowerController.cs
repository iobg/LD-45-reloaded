using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{	
	public float moveSpeed;
    public Vector2 position;
    public int verticalOffset;
    public int horizontalOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   float vertical = PlayerController.instance.y + verticalOffset;
        float horizontal = PlayerController.instance.x + horizontalOffset;
        Vector2 move = new Vector2(horizontal, vertical);
        GetComponent<Rigidbody2D>().MovePosition(move);
    }
}
