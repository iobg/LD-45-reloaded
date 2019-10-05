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
        position = GetComponent<Rigidbody2D>().position;

        if(position.x + horizontalOffset > move.x + horizontalOffset){
            move.x = position.x - (float)0.1;
            if(move.x - position.x < 0.2 && move.x -position.x > 0 || move.x - position.x > - 0.2 && move.x -position.x < 0 ){
                Debug.Log("CORRECTION");
                move.x = PlayerController.instance.x + horizontalOffset;
            }
        }

        if(position.x + horizontalOffset < move.x + horizontalOffset ){
            move.x = position.x + (float)0.1;
        }

        if(position.y + verticalOffset > move.y + verticalOffset){
            move.y = position.y - (float)0.1;            
            if(move.y - position.y < 0.2 && move.y -position.y > 0 || move.y - position.y > - 0.2 && move.y -position.y < 0 ){
                Debug.Log("CORRECTION");
                move.y = PlayerController.instance.y + verticalOffset;
            }
        }


        if(position.y + verticalOffset < move.y + verticalOffset ){
            move.y = position.y + (float)0.1;
        }

        GetComponent<Rigidbody2D>().MovePosition(move);
    }
}
