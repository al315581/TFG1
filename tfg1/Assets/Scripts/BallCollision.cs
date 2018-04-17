using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {
    public BallScript ballScript;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !ballScript.ballStopped )
        {
            //Destroy(other.gameObject);
            GamMan.state = GamMan.stateOfMatch.endPoint;

            //Now just add a point to the respective player that wins the round
            //Also, we change the point where the ball will spawn
            if(other.gameObject.name == "P2")
            {
                ScoreP1Script.scoreP1 += 1;
                GamMan.point = GamMan.pointOfStart.player2;
            }
            else
            {
                ScoreP2Script.scoreP2 += 1;
                GamMan.point = GamMan.pointOfStart.player1;
            }
            
        }
    }
}
