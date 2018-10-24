using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{

    // Use this for initialization
    public Vector3 startPoint, endPoint;
    float currentPercentage;
    public float speed = 1f;
    public float startTime;
    public float journeyLength;

    public float waitTime = 1f;
    public float waitAmt = 0;

    //Conversion for positions
    private Vector3 startAlt, endAlt;

    public bool connected = false;
    public SliderJoint2D joint;
    public float breakingDist = 1.75f;

    void Start()
    {
        startPoint = transform.GetChild(0).position;
        endPoint = transform.GetChild(1).position;

        //Grabbed from unity documentation
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPoint, endPoint);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != endPoint)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;


            //currentPercentage = Mathf.PingPong(Time.time, speed);
            transform.position = Vector3.Lerp(startPoint, endPoint, fracJourney);

        }
        //We have completed our journey
        else if (transform.position == endPoint)
        {
            if (waitAmt < waitTime)
            {
                waitAmt += Time.deltaTime;
            }
            else
            {
                waitAmt = 0;
                reversePlatform();
            }
        }
        

        if (connected == true && Input.GetKey(KeyCode.Space))
        {

            joint.connectedBody = null;
        }
        if(joint != null)
        {
            if (joint.connectedBody != null
             && Mathf.Abs(transform.position.x - joint.connectedBody.transform.position.x) >= breakingDist)
            {
                joint.connectedBody = null;
            }

        }
    }


    void reversePlatform()
    {
        startAlt = endPoint;
        endAlt = startPoint;
        startPoint = startAlt;
        endPoint = endAlt;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPoint, endPoint);

    }

    //Massive thanks to: http://spacelizardstudio.com/devblog/index.php/2016/03/02/unity-a-guide-to-moving-platforms/
    //For the basic structure of this code
    //And for telling me that the slider joint exists
    //Slider joint documentation: https://docs.unity3d.com/Manual/class-SliderJoint2D.html

    void ConnectTo(Rigidbody2D character)
    {
        joint = GetComponent<SliderJoint2D>();
        joint.connectedBody = character;
        connected = true;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    void OnCollisionEnter2D(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Vector2 direction = collision.contacts[0].normal;
            if (Mathf.Approximately(direction.y, -1f))
            {
                // upon collision, record our distance and use it as an offset.
                joint.anchor = new Vector2(0f, Mathf.Abs(transform.position.y - collision.transform.position.y));
                if (collision.gameObject.tag == "Player")
                {
                    ConnectTo(collision.gameObject.GetComponent<Rigidbody2D>());
                }
            }
        }
    }
}
