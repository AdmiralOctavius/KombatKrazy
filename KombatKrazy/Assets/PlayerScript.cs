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
    
    public float sprintSpeed = 7f;
    public float walkSpeed = 6f;

    //Testing an acceleration speed
    public float accelSpeed = 10f;
    public float sprintAccelSpeed = 15f;

    //Jumping variables
    public float jumpForce = 300f;
    bool currentlyJumping;
    public float jumpFallSpeed = -10f;
    public float JumpTimeLim = 2;
    float jumpStartTime = 0f;
    float jumpEndTime = 0f;

    //Wallsliding variables
    public bool wallSliding = false;
    public float wallSlideSpeed = -1f;
    public float wallSlideSpeedSprint = -0.75f;

    //This might be deprecated to just jump force
    public float wallSlideJumpY = 350f;
    public float wallSlideJumpX = 50f;
    public float wallSlideSprintJumpX = 150f;

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
                if (wallSliding)
                {
                    //Todo- setup raycast at each point and grab the opposite side of collider
                    //Can detect collision point instead as well-> https://answers.unity.com/questions/783377/detect-side-of-collision-in-box-collider-2d.html
                    //Raycast may be easier?
                    rb.AddForce(new Vector2(-sprintAccelSpeed/2, 0));
                    rb.velocity = new Vector3(rb.velocity.x, wallSlideSpeedSprint, 0);
                }
                else
                {

                    if (rb.velocity.x > (-sprintSpeed))
                    {
                        rb.AddForce(new Vector2(-sprintAccelSpeed, 0));
                    }
                    else
                    {
                        rb.velocity = new Vector3(-sprintSpeed, rb.velocity.y, 0);
                    }
                }
            }

            else if (Input.GetKey(KeyCode.D))
            {
                if (wallSliding)
                {
                    rb.AddForce(new Vector2(-sprintAccelSpeed/2, 0));
                    rb.velocity = new Vector3(rb.velocity.x, wallSlideSpeedSprint, 0);
                }
                else
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

        }
        else
        {
            //Walking
            //Goal: check to see if the velocity of the rigidbody is greater than the walkspeed, if it is then add the walkspeed force to the game object.
            //Reasons: We want this so we can have deceleration on moving
            if (Input.GetKey(KeyCode.A))
            {
                if (wallSliding)
                {
                    RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.right, 13f, 8);
                    //
                    //Physics2D.OverlapBox ()
                    //
                    //
                    //Vector3.Dot
                    //
                    if (ray.collider != null)
                    {
                        Debug.Log(ray.collider.tag);

                        if (ray.collider.tag == "StdWall")
                        {
                            Debug.Log("Got here");
                            rb.AddForce(new Vector2(-accelSpeed, 0));
                            wallSliding = false;
                        }
                        else
                        {
                            rb.velocity = new Vector3(rb.velocity.x, wallSlideSpeed, 0);

                        }
                    }
                    else{
                        Debug.Log("No go");
                    }
                }
                else
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
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (wallSliding)
                {
                    
                    rb.velocity = new Vector3(rb.velocity.x, wallSlideSpeed, 0);
                }
                else
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!currentlyJumping)
            {
                //rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
                rb.AddForce(new Vector2(0, jumpForce));
                currentlyJumping = true;
                jumpStartTime = Time.time;

            }
            if (wallSliding)
            {
                rb.AddForce(new Vector2(wallSlideJumpX, wallSlideJumpY));
            }
        }
        else if (Input.GetKey(KeyCode.Space) && currentlyJumping)
        {
            if(rb.velocity.y < 0)
            {
                if(rb.velocity.y > jumpFallSpeed)
                {
                    //Do nothing
                }
                //Here we're establishing a cap on downward velocity
                if(rb.velocity.y < jumpFallSpeed)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpFallSpeed, 0);
                }
            }
            else
            {
                //Could setup a cap on fall speed, but in testing I liked being able to set the fall speed by own accord
                //Deprecated, but kept as artifact
                if((jumpEndTime - jumpStartTime) > JumpTimeLim)
                {

                }
            }
        }
        /*
        if (currentlyJumping)
        {
            jumpEndTime = Time.time;
        }
        else
        {
            jumpEndTime = 0;
            jumpStartTime = 0;
        }*/
    }

    //Some bug here where it sometimes allows for the player to double jump

    void OnTriggerEnter2D(Collider2D col)
    {
        currentlyJumping = false;
    }


    //This method is a little finnicky
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "StdWall")
        {
            wallSliding = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "StdWall")
        {
            wallSliding = false;
        }
    }







}
