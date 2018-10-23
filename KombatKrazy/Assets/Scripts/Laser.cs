using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public AudioClip laserStart;
    public AudioClip fullLaserClip;
    public AudioSource aPlayer;
    int toggler;
    GameObject player;
    public float LaserSpeed = 1f;
	// Use this for initialization
	void Start () {
        toggler = 1;
        
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Animator>().speed = LaserSpeed;
	}

    void startLzr()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        aPlayer.clip = laserStart;
        aPlayer.Stop();
        aPlayer.Play();
    }

    void fullLaser()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        aPlayer.clip = fullLaserClip;
        aPlayer.Stop();
        aPlayer.Play();
    } 

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            col.gameObject.GetComponent<HealthBar>().healthReduce(1);
            LaserHitSq();
        }
    }

    void LaserHitSq()
    {
        Invoke("CallHit", 0f);
        Invoke("CallHit", 0.1f);
        Invoke("CallHit", 0.2f);
        Invoke("CallHit", 0.3f);
        Invoke("CallHit", 0.4f);
        Invoke("CallHit", 0.5f);
    }

    void CallHit()
    {

        if (toggler == 1)
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
