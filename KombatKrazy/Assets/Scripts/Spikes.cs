using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    GameObject player;
    int toggler;

	// Use this for initialization
	void Start () {
        toggler = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            col.gameObject.GetComponent<HealthBar>().healthReduce(1);
            col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 10);
            Invoke("CallHit", 0f);
            Invoke("CallHit", 0.1f);
            Invoke("CallHit", 0.2f);
            Invoke("CallHit", 0.3f);
            Invoke("CallHit", 0.4f);
            Invoke("CallHit", 0.5f);
        }
    }

    void CallHit()
    {
        
        if(toggler == 1)
        {
            player.GetComponent<PlayerScript>().PlayerHit(toggler);
            toggler = 0;
        }
        else
        {
            player.GetComponent<PlayerScript>().PlayerHit(toggler);
            toggler = 1;
        }
        

    }
}
