using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedShoes : MonoBehaviour {

    //------This block taken from the enemy script to get a nice small bob-----//

    public Vector3 startPoint, endPoint;
    float currentPercentage = 0;

    void Start()
    {

        startPoint = transform.GetChild(0).position;
        endPoint = transform.GetChild(1).position;
    }

    void Update()
    {
        currentPercentage = Mathf.PingPong(Time.time, 1);
        transform.position = Vector3.Lerp(startPoint, endPoint, currentPercentage);
    }

    //----------//

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerScript>().sprintBoots = true;
            FindObjectOfType<GoalPost>().gameObject.GetComponent<GoalPost>().HasSprintBoots = true;
            this.gameObject.SetActive(false);
        }

    }
}
