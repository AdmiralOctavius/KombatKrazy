using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour {

    public GameObject player;
    public Sprite[] HealthSprites;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
            gameObject.GetComponent<SpriteRenderer>().sprite = HealthSprites[player.GetComponent<HealthBar>().health];
	}
}
