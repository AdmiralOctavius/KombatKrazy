using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public int health = 5;
    public bool dead;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void healthReduce(int hpDrain = 1)
    {
        if(health - hpDrain < 0)
        {

        }
        else
        {
            health -= hpDrain;

        }
        if(health <= 0)
        {
            health = 0;
            dead = true;
            if(gameObject.tag == "Player")
            {
                gameObject.GetComponent<PlayerScript>().enabled = false;
                Debug.Log("We died");
            }
        }

        
    }
}
