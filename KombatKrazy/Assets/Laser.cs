using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public AudioClip laserStart;
    public AudioClip fullLaserClip;
    public AudioSource aPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void startLzr()
    {
        aPlayer.clip = laserStart;
        aPlayer.Play();
    }

    void fullLaser()
    {
        aPlayer.clip = fullLaserClip;
        aPlayer.Play();
    } 
}
