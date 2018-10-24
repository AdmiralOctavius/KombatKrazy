using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour {

    //public Text timerCount;
    public GameObject globals;
    public int timeLeftLocal;

    public int minutes;
    public int seconds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeftLocal = (int)globals.GetComponent<Globals>().timeLeft;
        minutes = timeLeftLocal / 60;
        seconds = timeLeftLocal % 60;
        gameObject.GetComponent<Text>().text = minutes + " " + seconds;
	}
}
