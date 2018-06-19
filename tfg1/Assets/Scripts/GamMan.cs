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

    public float ballTime = 10f;
    public float timerBallTime = 0f;
    private BoxCollider bc;

    public int currentSideOfBall;
    public int previousSideOfBall;

    public GameObject leftPlane, rightPlane;
    public GameObject P1, P2;
    public Transform P1initialPosition, P2initialPosition;
    

	// Use this for initialization
	void Start () {
        TimerScript.matchTime = 60;
        ScoreP1Script.scoreP1 = 0;
        ScoreP2Script.scoreP2 = 0;
        state = stateOfMatch.notStarted;
        point = pointOfStart.player1;
        velLev = velocityLevel.level1;
        bc = GetComponent<BoxCollider>();

        P1.transform.position = P1initialPosition.position;
        P2.transform.position = P2initialPosition.position;
	}
	
	// Update is called once per frame
	void Update () {
        CheckPositionOfBall();
        CheckWallsOpen();
        TimerScript.matchTime -= Time.deltaTime;
        switch (state){
            case stateOfMatch.notStarted:
                ball.transform.position = startPointP1.position;
                currentSideOfBall = 1;
                previousSideOfBall = 1;

                if (!ball.GetComponent<BallScript>().ballStopped)
                {
                    state = stateOfMatch.running;
                    ball.GetComponent<BallScript>().hitted = true;
                }
                print("no ha empezado");
                break;

            case stateOfMatch.running:
                //doChangesWhileRunning();
                //print("empezamos");
                HotBallLogic();



                break;

            case stateOfMatch.endPoint:

                ball.GetComponent<BallScript>().ballStopped = true;
                ball.GetComponent<BallScript>().hitted = false;


                if (point == pointOfStart.player1)
                {
                    ball.transform.position = startPointP1.position;
                    PM.StartParticlesP1();
                    currentSideOfBall = 1;
                    previousSideOfBall = 1;
                }
                else
                {
                    ball.transform.position = startPointP2.position;
                    PM.StartParticlesP2();
                    currentSideOfBall = 2;
                    previousSideOfBall = 2;
                }
                StartCoroutine(WaitBetweenRounds());
                state = stateOfMatch.startPoint;
                ball.GetComponent<BallScript>().ResetVelocity();
                ball.GetComponent<BallScript>().ResetMaterial();
                break;

            case stateOfMatch.startPoint:
                //print("estamos aqui");
                if (!ball.GetComponent<BallScript>().ballStopped)
                {
                    state = stateOfMatch.running;
                    ball.GetComponent<BallScript>().hitted = true;

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

    IEnumerator WaitBetweenRounds()
    {
        print("empieza la espera");
        yield return new WaitForSeconds(3f);
        RestartPlayers();
        print("fin espera");
    }
    IEnumerator WaitBeforeWakingUp()
    {
        yield return new WaitForSeconds(1.5f);
        RestartMovementPlayers();
        
    }

    private void RestartPlayers()
    {
        if (P1.GetComponent<PlayerMovement>().defeated)
        {
            P1.GetComponent<PlayerMovement>().defeated = false;
            P1.transform.position = P1initialPosition.position;
            P1.GetComponent<PlayerMovement>().RestartAnimations();
            //P1.GetComponent<PlayerMovement>().isOnGround = false; 
        }
        if (P2.GetComponent<PlayerMovement>().defeated)
        {
            P2.GetComponent<PlayerMovement>().defeated = false;
            P2.transform.position = P2initialPosition.position;
            P2.GetComponent<PlayerMovement>().RestartAnimations();
            //P2.GetComponent<PlayerMovement>().isOnGround = false;

        }
        StartCoroutine(WaitBeforeWakingUp());
    }
    private void RestartMovementPlayers()
    {
        P1.GetComponent<PlayerMovement>().isOnGround = false;
        P2.GetComponent<PlayerMovement>().isOnGround = false;
    }

    private void CheckPositionOfBall()
    {
        if (ball.transform.position.x < 0)
        {
            currentSideOfBall = 1;
            if(previousSideOfBall == 2)
            {
                ResetTimerHotBall();
                ResetHittedBall();
                previousSideOfBall = 1;
            }
        }
        else if (ball.transform.position.x > 0)
        {
            currentSideOfBall = 2;
            if(previousSideOfBall == 1)
            {
                ResetTimerHotBall();
                ResetHittedBall();
                previousSideOfBall = 2;
            }
        }

            
    }

    private void CheckWallsOpen()
    {
        if(ball.GetComponent<BallScript>().hitted == true)
        {
            LetTheBallCross();
        }
        else
        {
            DontLetTheBallCross();
        }
    }

    private void HotBallLogic()
    {
        timerBallTime += Time.deltaTime;
        if(timerBallTime >= ballTime)
        {
            PM.HitPlayerParticles(ball.transform);
            state = stateOfMatch.endPoint;
            timerBallTime = 0;
            if(currentSideOfBall == 1)
            {
                point = pointOfStart.player2;
            }
            else
            {
                point = pointOfStart.player1;
            }
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

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            //print("está en el campo derecho");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
           // print("está en el campo izquierdo");
        }
    }

    private void ResetTimerHotBall()
    {
        timerBallTime = 0;
    }
    private void ResetHittedBall()
    {
        ball.GetComponent<BallScript>().hitted = false;
    }

    private void LetTheBallCross()
    {
        leftPlane.SetActive(false);
        rightPlane.SetActive(false);
    }
    private void DontLetTheBallCross()
    {
        leftPlane.SetActive(true);
        rightPlane.SetActive(true);
    }
}
