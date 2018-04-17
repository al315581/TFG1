using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamMan : MonoBehaviour {

    public GameObject ball;
   public enum stateOfMatch : short { notStarted, running, endPoint, startPoint, endMatch};   //we use short for optimization
    public static stateOfMatch state;

    public enum pointOfStart : short { player1, player2 };
    public static pointOfStart point;

    public enum velocityLevel : short { level1, level2, level3, level4};
    public static velocityLevel velLev;


    public Transform startPointP1, startPointP2;

	// Use this for initialization
	void Start () {
        TimerScript.matchTime = 60;
        ScoreP1Script.scoreP1 = 0;
        ScoreP2Script.scoreP2 = 0;
        state = stateOfMatch.notStarted;
        point = pointOfStart.player1;
        velLev = velocityLevel.level1;
        
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
                //print("empezamos");
                break;

            case stateOfMatch.endPoint:

                ball.GetComponent<BallScript>().ballStopped = true;

                if (point == pointOfStart.player1)
                {
                    ball.transform.position = startPointP1.position;
                }
                else
                {
                    ball.transform.position = startPointP2.position;
                }
                
                state = stateOfMatch.startPoint;
                ball.GetComponent<BallScript>().ResetVelocity();
                break;

            case stateOfMatch.startPoint:
                if (!ball.GetComponent<BallScript>().ballStopped)
                {
                    state = stateOfMatch.running;
                }
                break;
        }
	}
}
