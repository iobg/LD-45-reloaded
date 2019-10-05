using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{	

    // Public variables
	public float moveSpeed = 10f;
    public Vector2 followerOffset;

    // Private variables
    private Vector2 playerPosition;
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        //Navmesh code to lock rotation
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {   

        playerPosition = PlayerController.instance.GetComponent<Rigidbody2D>().position;
        Vector2 placeToBe = playerPosition + followerOffset;

        // Navmesh move to place
        agent.SetDestination(placeToBe);

    }
}
