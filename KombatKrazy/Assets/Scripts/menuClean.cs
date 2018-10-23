using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuClean : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.HasKey("HasDoubleJump").ToString());
        Debug.Log(PlayerPrefs.HasKey("HasFarWallJump").ToString());
        Debug.Log(PlayerPrefs.HasKey("HasSpeedBoots").ToString());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
