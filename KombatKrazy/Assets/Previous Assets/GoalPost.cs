using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour {

    public bool HasWallJump = false;
    public bool HasSprintBoots = false;
    public bool HasDoubleBoots = false;
    public string NextLevel = "menu";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (HasWallJump)
            {
                PlayerPrefs.SetInt("HasFarWallJump", 1);
                PlayerPrefs.Save();
            }
            if (HasSprintBoots)
            {
                PlayerPrefs.SetInt("HasSpeedBoots", 1);
                PlayerPrefs.Save();
            }
            if (HasDoubleBoots)
            {
                PlayerPrefs.SetInt("HasDoubleJump", 1);
                PlayerPrefs.Save();
            }

            SceneManager.LoadScene(NextLevel);
        }
    }
    
}
