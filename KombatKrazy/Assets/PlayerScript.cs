/*
 * This is a fresh Player Script, built with the purpose of:
 * -Allowing for wall sliding
 * -Sprinting
 * -Double jumping
 * 
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float fallSpeed = 5f;
    public float wallSlideSpeed = 1f;
    public float sprintSpeed = 7f;
    public float walkSpeed = 6f;

    //Testing an acceleration speed
    public float accelSpeed = 10f;
    public float sprintAccelSpeed = 15f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();


        //Sprinting
        //Goals: Setup a responsive sprint that accelerates faster than walking
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Got A key");
                if (rb.velocity.x > (-sprintSpeed))
                {
                    rb.AddForce(new Vector2(-sprintAccelSpeed, 0));
                }
                else
                {
                    rb.velocity = new Vector3(-sprintSpeed, rb.velocity.y, 0);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.x < (sprintSpeed))
                {
                    rb.AddForce(new Vector2(sprintAccelSpeed, 0));
                }
                else
                {
                    rb.velocity = new Vector3(sprintSpeed, rb.velocity.y, 0);
                }
            }
        }
        else
        {
            //Walking
            //Goal: check to see if the velocity of the rigidbody is greater than the walkspeed, if it is then add the walkspeed force to the game object.
            //Reasons: We want this so we can have deceleration on moving
            if (Input.GetKey(KeyCode.A))
            {
                if (rb.velocity.x > (-walkSpeed))
                {
                    rb.AddForce(new Vector2(-accelSpeed, 0));
                }
                else
                {
                    rb.velocity = new Vector3(-walkSpeed, rb.velocity.y, 0);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.x < (walkSpeed))
                {
                    rb.AddForce(new Vector2(accelSpeed, 0));
                }
                else
                {
                    rb.velocity = new Vector3(walkSpeed, rb.velocity.y, 0);
                }
            }
        }
        
    }








}
