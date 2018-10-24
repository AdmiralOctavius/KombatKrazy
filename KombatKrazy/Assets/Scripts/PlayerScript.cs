/*
 * This is a fresh Player Script, built with the purpose of:
 * -Allowing for wall sliding
 * -Sprinting
 * -Double jumping
 * 
 * 
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float fallSpeed = 5f;
    
    public float sprintSpeed = 7f;
    public float walkSpeed = 6f;

    //Testing an acceleration speed
    public float accelSpeed = 10f;
    public float sprintAccelSpeed = 15f;

    //Jumping variables
    public float jumpForce = 300f;
    public bool currentlyJumping;
    public float jumpFallSpeed = -10f;
    public float JumpTimeLim = 2;
    float jumpStartTime = 0f;
    float jumpEndTime = 0f;

    //Wallsliding variables
    public bool wallSliding = false;
    public float wallSlideSpeed = -1f;
    public float wallSlideSpeedSprint = -0.75f;

    //This might be deprecated to just jump force
    public float wallSlideJumpY = 7;
    public float wallSlideJumpX = 2;
    public float wallSlideSprintJumpX = 150f;

    //WallSlide Disconnect variables
    public bool right = false;
    public bool top = false;

    //Walljump testing
    public bool inWallJump = false;

    //WallJump Variables ext
    public float wallJumpX = 5f;
    public float wallJumpOffX = 3f;
    public float wallJumpOffY = 3f;
    public float wallLeapX = 5f;
    public float wallLeapY = 5f;

    //WallSlide stick
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    //Player fall bool
    bool playerFallingHeld = false;

    //PowerUps bools
    public bool farWallJump = false;
    public bool doubleJumpBoots = false;
    public bool notDoubleJumped = false;
    public float doubleJumpSpeed = 2.5f;
    public bool sprintBoots = false;

    //Animator
    public Animator anim;
    SpriteRenderer spriteMan;

    //Jump Sound
    AudioSource AudioPlayer;
    
    // Use this for initialization
    void Start () {
        spriteMan = gameObject.GetComponent<SpriteRenderer>();
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        Scene scene;
        scene = SceneManager.GetActiveScene();
        if (scene.name == "level1" || scene.name == "tutorialLevel")
        {
            Debug.Log("got here");
            PlayerPrefs.DeleteAll();
        }
        


        int result = 0;
        if (PlayerPrefs.HasKey("HasFarWallJump"))
        {
            result = PlayerPrefs.GetInt("HasFarWallJump");
        }
        if(result == 1)
        {
            farWallJump = true;
        }

        result = 0;
        if (PlayerPrefs.HasKey("HasSpeedBoots"))
        {
            result = PlayerPrefs.GetInt("HasSpeedBoots");
        }
        if(result == 1)
        {
            sprintBoots = true;
        }

        result = 0;
        if (PlayerPrefs.HasKey("HasDoubleJump"))
        {
            result = PlayerPrefs.GetInt("HasDoubleJump");
        }
        if (result == 1)
        {
            doubleJumpBoots  = true;
        }

    }
	
	// Update is called once per frame
	void Update () {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(playerFallingHeld == false)
            {
                playerFallingHeld = true;
            }
            else
            {
                playerFallingHeld = false;
            }
            if (wallSliding)
            {
                AudioPlayer.Play();
                inWallJump = true;
                wallSliding = false;
                currentlyJumping = true;
                rb.velocity = new Vector3(0, 0, 0);
                
                if (Input.GetKey(KeyCode.A))
                {
                    spriteMan.flipX = true;
                    if(right == true)
                    {
                        //Sticky jump
                        //rb.velocity = new Vector2(wallSlideJumpX,wallSlideJumpY);
                        rb.AddForce(new Vector2(wallSlideJumpX, wallSlideJumpY), ForceMode2D.Impulse);
                        Debug.Log("Sticky jump performed");
                    }
                    else
                    {
                        if (farWallJump)
                        {
                            //Far Jump
                            rb.AddForce(new Vector2(-wallLeapX, wallLeapY), ForceMode2D.Impulse);
                            Debug.Log("Far Jump performed : " + rb.velocity.y.ToString());

                        }
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    spriteMan.flipX = false;
                    if (right == true)
                    {
                        if (farWallJump)
                        {
                            //Far Jump
                            rb.AddForce(new Vector2(wallLeapX, wallLeapY), ForceMode2D.Impulse);
                            Debug.Log("Far Jump performed : " + rb.velocity.y.ToString());

                        }
                    }
                    else
                    {
                        //Sticky Jump
                        rb.AddForce(new Vector2(-wallSlideJumpX, wallSlideJumpY), ForceMode2D.Impulse);
                        Debug.Log("Sticky jump performed");
                        
                    }
                    
                }
                else
                {
                    if(right == true)
                    {
                        rb.AddForce(new Vector2(wallJumpOffX, wallJumpOffY), ForceMode2D.Impulse);
                        Debug.Log("Simple wall jump performed");
                    }
                    else
                    {
                        rb.AddForce(new Vector2(-wallJumpOffX, wallJumpOffY), ForceMode2D.Impulse);
                        Debug.Log("Simple wall jump performed");
                    }

                }
                
                Invoke("NotInWallJump", 0.5f);
            }

            if (!currentlyJumping)
            {
                AudioPlayer.Play();
                rb.AddForce(new Vector2(0, jumpForce));
                currentlyJumping = true;
                jumpStartTime = Time.time;               
            }

            if(currentlyJumping && doubleJumpBoots && notDoubleJumped == false)
            {

            }

        }

        
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();




        //Sprinting
        //Goals: Setup a responsive sprint that accelerates faster than walking
        if (Input.GetKey(KeyCode.LeftShift) && sprintBoots)
        {
           
                if (Input.GetKey(KeyCode.A))
                {
                
                if (inWallJump)
                    {
                    spriteMan.flipX = true;
                    rb.AddForce(new Vector2(-sprintSpeed / 2, 0));
                        
                    }
                    else if(wallSliding)
                    {
                    spriteMan.flipX = false;
                    if (right == true)
                        {                        
                            //Todo- setup raycast at each point and grab the opposite side of collider
                            //Can detect collision point instead as well-> https://answers.unity.com/questions/783377/detect-side-of-collision-in-box-collider-2d.html
                            //Raycast may be easier?
                            //Solution found was dectecting the sides of the collision when we collide
                            rb.velocity = new Vector2(0, wallSlideSpeedSprint);
                        }
                        else
                        {
                        
                        if (timeToWallUnstick > 0)
                            {
                                timeToWallUnstick -= Time.deltaTime;
                            }
                            else
                            {
                                timeToWallUnstick = wallStickTime;
                                if (right == false)
                                {
                                    //rb.velocity = new Vector2(-sprintSpeed, 0);
                                }
                            }


                        }
                }
                    else
                    {
                    spriteMan.flipX = true;
                    if (rb.velocity.x > -sprintSpeed)
                        {
                            rb.AddForce(new Vector2(-sprintAccelSpeed, 0));
                        }
                        else
                        {
                            rb.AddForce(new Vector2(-sprintSpeed, 0));
                        }
                    }
                }

                else if (Input.GetKey(KeyCode.D))
                {
               
                if (inWallJump)
                    {
                    spriteMan.flipX = false;
                    rb.AddForce(new Vector2(-sprintSpeed / 2, 0));
                    }
                    else if (wallSliding)
                    {
                    spriteMan.flipX = true;
                    if (right == false)
                        {                                          
                            rb.velocity = new Vector2(0, wallSlideSpeedSprint);
                        }
                        else
                        {
                            if (timeToWallUnstick > 0)
                            {
                                timeToWallUnstick -= Time.deltaTime;
                            }
                            else
                            {
                                timeToWallUnstick = wallStickTime;
                                if (right == true)
                                {
                                    //rb.velocity = new Vector2(sprintSpeed, 0);
                                }
                            }
                            
                            
                        }

                }
                    else
                    {
                    spriteMan.flipX = false;
                    if (rb.velocity.x < sprintSpeed)
                        {
                            rb.AddForce(new Vector2(sprintAccelSpeed, 0));
                        }
                        else
                        {
                            rb.AddForce(new Vector2(sprintSpeed, 0));
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
            
                if (inWallJump)
                {
                    spriteMan.flipX = true;
                    rb.AddForce(new Vector2(-accelSpeed/1.5f, 0));
                }
                else if (wallSliding)
                {
                    spriteMan.flipX = false;
                    if (right == true)
                    { 
                        rb.velocity = new Vector2(0, wallSlideSpeed);
                    }
                    else
                    {
                         if (timeToWallUnstick > 0)
                         {
                             timeToWallUnstick -= Time.deltaTime;
                         }
                         else
                         {
                             timeToWallUnstick = wallStickTime;
                             if (right == false)
                             {
                                 rb.AddForce(new Vector2(-accelSpeed, 0));
                             }                            
                         }
                    }
                    
                }
                else
                {
                    spriteMan.flipX = true;
                    if (rb.velocity.x > (-walkSpeed))
                    {
                        rb.AddForce(new Vector2(-accelSpeed, 0));
                    }
                    else
                    {
                        rb.AddForce(new Vector2(-walkSpeed, 0));                       
                    }
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                
                if (inWallJump)
                {
                    spriteMan.flipX = false;
                    rb.AddForce(new Vector2(accelSpeed /1.5f, 0));
                }
                else if (wallSliding)
                {
                    spriteMan.flipX = true;
                    if (right == false)
                    {
                        rb.velocity = new Vector2(0, wallSlideSpeed);
                    }
                    else
                    {
                        if (timeToWallUnstick > 0)
                        {
                            timeToWallUnstick -= Time.deltaTime;
                        }
                        else
                        {
                            timeToWallUnstick = wallStickTime;
                            if (right == true)
                            {
                                rb.AddForce(new Vector2(accelSpeed, 0));
                            }
                        }
                    }                   
                }                
                else
                {
                    spriteMan.flipX = false;
                    if (rb.velocity.x < (walkSpeed))
                    {
                        rb.AddForce(new Vector2(accelSpeed, 0));
                    }
                    else
                    {
                        rb.AddForce(new Vector2(walkSpeed, 0));                        
                    }
                }
            }
            else
            {

            }
        }

        

        if (Input.GetKey(KeyCode.Space) && currentlyJumping)
        {
            if (rb.velocity.y < 0)
            {
                if (rb.velocity.y > jumpFallSpeed)
                {
                    //Do nothing
                }
                //Here we're establishing a cap on downward velocity if held button
                if (rb.velocity.y < jumpFallSpeed && playerFallingHeld == true)
                {

                    //rb.velocity = new Vector3(rb.velocity.x, jumpFallSpeed, 0);
                    rb.AddForce(new Vector2(0, jumpFallSpeed), ForceMode2D.Force);
                    
                }
            }
            else
            {
                //Could setup a cap on fall speed, but in testing I liked being able to set the fall speed by own accord
                //Deprecated, but kept as artifact
                if ((jumpEndTime - jumpStartTime) > JumpTimeLim)
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

        //Debug.Log(rb.velocity.x.ToString());

        if(rb.velocity != new Vector2(0, 0))
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        if (wallSliding)
        {
            anim.SetBool("wallSliding", true);

        }
        else
        {
            anim.SetBool("wallSliding", false);
        }
    }

    //Some bug here where it sometimes allows for the player to double jump

    void OnTriggerEnter2D(Collider2D col)
    {
        currentlyJumping = false;
        wallSliding = false;
        playerFallingHeld = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        currentlyJumping = false;
        wallSliding = false;
        playerFallingHeld = false;
    }


    //This method is a little finnicky
    void OnCollisionEnter2D(Collision2D col)
    {
        //This collision detection found from: https://answers.unity.com/questions/783377/detect-side-of-collision-in-box-collider-2d.html
        Collider2D collider = col.collider;
        if(col.gameObject.tag == "StdWall")
        {
            wallSliding = true;
            currentlyJumping = false;
            
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = collider.bounds.center;
		//This is the important bit here. Essentially stating that right is true 
		//when the opposing object's X or Y CENTER value is greater than the Player's
            right = contactPoint.x > center.x;
            top = contactPoint.y > center.y;

            //Debug.Log("Output for wall detect: " + contactPoint.ToString() + " | " + center.ToString() + " | " + right + " | " + top);
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "StdWall")
        {
            wallSliding = false;
        }
    }

    void NotInWallJump()
    {
        inWallJump = false;
    }

    public void PlayerHit(int input)
    {
        if(input == 1)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }




}
