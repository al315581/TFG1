using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamMan : MonoBehaviour {

    public GameObject ball;

	// Use this for initialization
	void Start () {
        TimerScript.matchTime = 60;
        ScoreP1Script.scoreP1 = 0;
        ScoreP2Script.scoreP2 = 0;
	}
	
	// Update is called once per frame
	void Update () {
        TimerScript.matchTime -= Time.deltaTime;
	}
}
