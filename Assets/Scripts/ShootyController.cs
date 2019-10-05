using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyController : MonoBehaviour
{

	public GameObject crosshair;
	public Camera mainCamera;

    Vector3 mouseDirection;


    // Start is called before the first frame update
    void Start()
    {
    	mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    	Vector3 screenPosition = mainCamera.WorldToScreenPoint(crosshair.transform.position);
    	mouseDirection = Input.mousePosition - new Vector3(screenPosition.x, screenPosition.y, 0);

        
    }


    void FixedUpdate()
    {
        // Change position
        Vector2 currentPosition = transform.position;
        Vector2 crosshairPosition; crosshairPosition.x = crosshair.transform.position.x; crosshairPosition.y = crosshair.transform.position.y;
        currentPosition.x = mouseDirection.x / 150.0f;
        currentPosition.y = mouseDirection.y / 150.0f;
        currentPosition = Vector2.ClampMagnitude(currentPosition, 2);
        transform.SetPositionAndRotation(crosshairPosition + currentPosition, Quaternion.Euler(0, 0, 0));
    }



}
