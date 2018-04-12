using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamMan : MonoBehaviour {

    public GameObject ball;
    enum stateOfMatch : short { notStarted, running, endPoint, startPoint, endMatch};   //we use short for optimization
    stateOfMatch state;

    public Transform startPointP1, startPointP2;

	// Use this for initialization
	void Start () {
        TimerScript.matchTime = 60;
        ScoreP1Script.scoreP1 = 0;
        ScoreP2Script.scoreP2 = 0;
        state = stateOfMatch.notStarted;
        
	}
	
	// Update is called once per frame
	void Update () {
        TimerScript.matchTime -= Time.deltaTime;


        switch (state){
            case stateOfMatch.notStarted:
                ball.transform.position = startPointP1.position;
                if (!ball.GetComponent<BallScript>().ballStopped)
                {
                    state = stateOfMatch.running;
                }
                print("no ha empezado");
                break;
            case stateOfMatch.running:
                print("empezamos");
                break;
        }
	}
}
