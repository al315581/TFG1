using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {
    public BallScript ballScript;
    public ParticleManager PM;
    private GameObject ball;
    
	// Use this for initialization
	void Start () {
        ball = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !ballScript.ballStopped )
        {
            FindObjectOfType<AudioManager>().PlayRandomPitch("ExplosionSound");

            other.gameObject.GetComponent<PlayerMovement>().defeated = true;
            other.gameObject.GetComponent<PlayerMovement>().isHitting = false;

            //Fire the particles
            PM.HitPlayerParticles(other.transform);
            CalculateDirectionOfHit(other.transform, other.gameObject);

            GetComponentInParent<BallScript>().gamMan.shakeCameraPlayerDead();
            //Destroy(other.gameObject);
            GamMan.state = GamMan.stateOfMatch.endPoint;

            //Now just add a point to the respective player that wins the round
            //Also, we change the point where the ball will spawn
            if(other.gameObject.name == "P2")
            {
                ScoreP1Script.scoreP1 += 1;
                GamMan.point = GamMan.pointOfStart.player2;
                GameObject.FindObjectOfType<GamMan>().DissolveManagerP2();

            }
            else
            {
                ScoreP2Script.scoreP2 += 1;
                GamMan.point = GamMan.pointOfStart.player1;
                GameObject.FindObjectOfType<GamMan>().DissolveManagerP1();

            }

        }
    }

    private void CalculateDirectionOfHit(Transform aux, GameObject go)
    {
        float result;
        result = Vector3.Angle(ball.transform.forward, aux.forward);
        //print(result);
        if (result < 90) {
            //print("detrás");
            go.GetComponent<PlayerMovement>().HittedBack();

        } 
        else
        {
            go.GetComponent<PlayerMovement>().HittedFront();
            //print("delante");

        } 
    }
}
