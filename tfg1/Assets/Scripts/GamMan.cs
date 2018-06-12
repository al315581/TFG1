using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GamMan : MonoBehaviour {

    public GameObject ball;
   public enum stateOfMatch : short { notStarted, running, endPoint, startPoint, endMatch};   //we use short for optimization
    public static stateOfMatch state;

    public enum pointOfStart : short { player1, player2 };
    public static pointOfStart point;


   
    public enum velocityLevel : short { level1, level2, level3, level4};
    public static velocityLevel velLev;
    public float velocityLevel1Limit, velocityLevel2Limit, velocityLevel3Limit, velocityLevel4Limit;

    public List<float> cameraShakeValuesPlayerDead = new List<float>();
    public List<float> cameraShakeValuesLevel1 = new List<float>();


    public Transform startPointP1, startPointP2;
    public SkyboxChanger skyBoxChanger;
    public ParticleManager PM;

    

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
                doChangesWhileRunning();
                //print("empezamos");
                break;

            case stateOfMatch.endPoint:

                ball.GetComponent<BallScript>().ballStopped = true;

                if (point == pointOfStart.player1)
                {
                    ball.transform.position = startPointP1.position;
                    PM.StartParticlesP1();
                }
                else
                {
                    ball.transform.position = startPointP2.position;
                    PM.StartParticlesP2();
                }
                
                state = stateOfMatch.startPoint;
                ball.GetComponent<BallScript>().ResetVelocity();
                ball.GetComponent<BallScript>().ResetMaterial();
                break;

            case stateOfMatch.startPoint:
                //print("estamos aqui");
                if (!ball.GetComponent<BallScript>().ballStopped)
                {
                    state = stateOfMatch.running;
                }
                break;
        }
	}

    private void doChangesWhileRunning()
    {
        switch (velLev)
        {
            case velocityLevel.level1:
                if (ball.GetComponent<BallScript>().velocity > velocityLevel1Limit)
                {
                    velLev = velocityLevel.level2;
                    skyBoxChanger.ChangeSkybox();
                }
                break;

            case velocityLevel.level2:
                break;
        }
    }




    public void shakeCameraWithWall()
    {
        switch (velLev)
        {
            case velocityLevel.level1:
                CameraShaker.Instance.ShakeOnce(cameraShakeValuesLevel1[0],cameraShakeValuesLevel1[1], cameraShakeValuesLevel1[2], cameraShakeValuesLevel1[3]);
                //CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.5f);
                
                break;
        }
    }


    public void shakeCameraPlayerDead()
    {
        CameraShaker.Instance.ShakeOnce(cameraShakeValuesPlayerDead[0], cameraShakeValuesPlayerDead[1], cameraShakeValuesPlayerDead[2], cameraShakeValuesPlayerDead[3]);
    }
}
