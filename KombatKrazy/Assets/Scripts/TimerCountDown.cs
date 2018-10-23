using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour {

    //public Text timerCount;
    public GameObject globals;
    public int timeLeftLocal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeftLocal = (int)globals.GetComponent<Globals>().timeLeft;
        gameObject.GetComponent<Text>().text = timeLeftLocal.ToString();
	}
}
