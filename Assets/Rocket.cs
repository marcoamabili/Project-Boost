﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rb;
	
	void Start ()
    {
        rb = GetComponent<Rigidbody>();      
    }
	

	void Update ()
    {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);

        } else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(- Vector3.forward);
        }
    }
}
