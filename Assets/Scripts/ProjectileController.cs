﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

	Rigidbody2D rigidbody2d;


	public float bulletSpeed = 300.0f; // Note that speed is affected by mass too
    public float bulletMass = 0.3f;
    public float bulletStun = 0.3f;
    public bool friendlyBullet;

    float bulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
    	rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.mass = bulletMass;
    }


    public void Launch(Vector2 direction, float bulletDamage)
    {
        direction.Normalize();
        rigidbody2d.AddForce(direction * bulletSpeed);
        this.bulletDamage = bulletDamage;
    }
}