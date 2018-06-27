using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GamMan : MonoBehaviour {

    public float MATCH_TIME = 100f;
    public float time_left = 100f;
    public bool match_started = false;
    private float timer;

    public GameObject ball;
    public enum stateOfMatch : short { notStarted, running, endPoint, startPoint, endMatch, cinematic, endGame};   //we use short for optimization
    public static stateOfMatch state;

    public enum pointOfStart : short { player1, player2 };
    public static pointOfStart point;



    public enum velocityLevel : short { level1, level2, level3, level4 };
    public static velocityLevel velLev;
    public float velocityLevel1Limit, velocityLevel2Limit, velocityLevel3Limit, velocityLevel4Limit;

    public List<float> cameraShakeValuesPlayerDead = new List<float>();
    public List<float> cameraShakeValuesLevel1 = new List<float>();
    public float hitShake1, hitShake2, hitShake3, hitShake4;


    public Transform startPointP1, startPointP2;
    public SkyboxChanger skyBoxChanger;
    public ParticleManager PM;

    public float ballTime = 10f;
    public float timerBallTime = 0f;
    public float timeInFlames = 4f;
    private BoxCollider bc;

    public int currentSideOfBall;
    public int previousSideOfBall;

    public GameObject leftPlane, rightPlane;
    public GameObject P1, P2;
    public Transform P1initialPosition, P2initialPosition;


    public Material[] P1Materials, P2Materials;
    public float fadeValue;
    public float maxValue = 4f;
    public float minValue = -1;
    public float velocityDissolve = 1f;
    public bool P1dissolving, P2dissolving;
    public bool dissolving = false;

    public float timeToStartRaining = 40f;
    public float timeToEndRaining = 20f;
    public float ppNightMax, ppNightMin;
    public GameObject postProcessingNight;

    public Transform WinPointP1, WinPointP2;

    // Use this for initialization
    void Start() {
        TimerScript.matchTime = MATCH_TIME;
        time_left = MATCH_TIME;
        ScoreP1Script.scoreP1 = 0;
        ScoreP2Script.scoreP2 = 0;
        //state = stateOfMatch.notStarted;
        state = stateOfMatch.cinematic;

        point = pointOfStart.player1;
        velLev = velocityLevel.level1;
        bc = GetComponent<BoxCollider>();

        P1.transform.position = P1initialPosition.position;
        P2.transform.position = P2initialPosition.position;


        fadeValue = minValue;
        for (int i = 0; i < P2Materials.Length; i++)
        {
            P1Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
        }

        for (int i = 0; i < P1Materials.Length; i++)
        {
            P2Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
        }
    }

    // Update is called once per frame
    void Update() {
        GameOverManager();
        

        CheckPositionOfBall();
        CheckWallsOpen();

        RainManager();
        PostProcessingManager();
        if (match_started)
        {
            time_left -= Time.deltaTime;
            TimerScript.matchTime = time_left;
            EnvironmentChanger();
        }


        switch (state) {
            case stateOfMatch.notStarted:
                
                ball.transform.position = startPointP1.position;
                currentSideOfBall = 1;
                previousSideOfBall = 1;

                if (!ball.GetComponent<BallScript>().ballStopped)
                {
                    state = stateOfMatch.running;
                    ball.GetComponent<BallScript>().hitted = true;
                }
                //print("no ha empezado");
                break;

            case stateOfMatch.running:
                //doChangesWhileRunning();
                //print("empezamos");
                HotBallLogic();



                break;

            case stateOfMatch.endPoint:

                ball.GetComponent<BallScript>().ballStopped = true;
                ball.GetComponent<BallScript>().hitted = false;
                RestartTimerBall();
                PM.EndFireTrail();



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
                GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("EnergyConcentration");

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

            case stateOfMatch.endGame:
                break;
        }
    }

    private void GameOverManager()
    {
        if (time_left <= 0)
        {
            time_left = 0;
            ball.GetComponent<BallScript>().velocity = 0;
            ball.transform.position = new Vector3(-10000, ball.transform.position.y, ball.transform.position.z);
            if (ScoreP1Script.scoreP1 == ScoreP2Script.scoreP2) //Draw
            {
                //completar empate
            }
            else
            {
                if(ScoreP1Script.scoreP1 > ScoreP2Script.scoreP2)   //win P1
                {
                    GameObject.FindObjectOfType<CameraHolderScript>().MoveToP1();
                }
                else  //win P2
                {
                    GameObject.FindObjectOfType<CameraHolderScript>().MoveToP2();
                    //PM.StartFireworksRed();
                }
            }
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




    public void DissolveManagerP1()
    {
        StartCoroutine(DisappearApearP1());
    }
    public void DissolveManagerP2()
    {
        StartCoroutine(DisappearApearP2());
    }

    IEnumerator DisappearApearP1()
    {
        StartCoroutine(P1Dissappearance());
        yield return new WaitForSeconds(3f);
        StartCoroutine(P1Appearance());

        yield return null;
    }

    IEnumerator DisappearApearP2()
    {
        StartCoroutine(P2Dissappearance());
        yield return new WaitForSeconds(3f);
        StartCoroutine(P2Appearance());

        yield return null;
    }

    IEnumerator WaitBeforeWakingUp()
    {

        yield return new WaitForSeconds(1.5f);
        RestartMovementPlayers();

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
            if (previousSideOfBall == 2)
            {
                ResetTimerHotBall();
                ResetHittedBall();
                previousSideOfBall = 1;
            }
        }
        else if (ball.transform.position.x > 0)
        {
            currentSideOfBall = 2;
            if (previousSideOfBall == 1)
            {
                ResetTimerHotBall();
                ResetHittedBall();
                previousSideOfBall = 2;
            }
        }


    }

    private void CheckWallsOpen()
    {
        if (ball.GetComponent<BallScript>().hitted == true)
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
        if (timerBallTime >= ballTime)
        {
            timerBallTime = 0;

            PM.HitPlayerParticles(ball.transform);
            state = stateOfMatch.endPoint;
            timerBallTime = 0;
            if (currentSideOfBall == 1)
            {
                P1.GetComponent<PlayerMovement>().defeated = true;

                P1.GetComponent<PlayerMovement>().HittedFront();
                DissolveManagerP1();
                point = pointOfStart.player2;
                PM.StartBurningGroundP1();
                FindObjectOfType<AudioManager>().PlayRandomPitch("ExplosionSound");

                ScoreP2Script.scoreP2++;
            }
            else
            {
                P2.GetComponent<PlayerMovement>().defeated = true;

                P2.GetComponent<PlayerMovement>().HittedFront();
                DissolveManagerP2();
                point = pointOfStart.player1;
                PM.StartBurningGroundP2();
                FindObjectOfType<AudioManager>().PlayRandomPitch("ExplosionSound");

                ScoreP1Script.scoreP1++;
            }
        }

        if (timerBallTime >= ballTime - timeInFlames)
        {
            PM.StartFireTrail();
        }
        else PM.EndFireTrail();

    }

    public void shakeCameraWithWall()
    {
        switch (velLev)
        {
            case velocityLevel.level1:
                CameraShaker.Instance.ShakeOnce(cameraShakeValuesLevel1[0], cameraShakeValuesLevel1[1], cameraShakeValuesLevel1[2], cameraShakeValuesLevel1[3]);
                //CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 0.5f);

                break;
        }
    }

    public void ShakeCameraPlayerHits()
    {
        CameraShaker.Instance.ShakeOnce(hitShake1,hitShake2,hitShake3,hitShake4);
        
    }

    public void shakeCameraPlayerDead()
    {
        CameraShaker.Instance.ShakeOnce(cameraShakeValuesPlayerDead[0], cameraShakeValuesPlayerDead[1], cameraShakeValuesPlayerDead[2], cameraShakeValuesPlayerDead[3]);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
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

    public void LetTheBallCross()
    {
        leftPlane.SetActive(false);
        rightPlane.SetActive(false);
    }
    public void DontLetTheBallCross()
    {
        leftPlane.SetActive(true);
        rightPlane.SetActive(true);
    }

    IEnumerator characterAppearance(int player)
    {
        while (fadeValue > minValue)
        {
            if (!dissolving)
            {
                fadeValue -= Time.deltaTime * velocityDissolve;

                if (player == 1)
                {
                    for (int i = 0; i < P1Materials.Length; i++)
                    {
                        P1Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
                    }
                    yield return null;
                }
                else
                {
                    for (int i = 0; i < P2Materials.Length; i++)
                    {
                        P2Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
                    }
                    yield return null;
                }
                dissolving = true;
            }
        }
        dissolving = false;
        yield return null;
    }

    IEnumerator P1Dissappearance()
    {
        while (fadeValue <= maxValue)
        {
            fadeValue += Time.deltaTime * velocityDissolve;
            //print(fadeValue);

            for (int i = 0; i < P1Materials.Length; i++)
            {
                P1Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
            }
            dissolving = true;
            yield return null;
        }
        dissolving = false;
        yield return null;
    }

    IEnumerator P1Appearance()
    {
        while (dissolving)
        {
            yield return null;
        }

        while (fadeValue > minValue)
        {
            fadeValue -= Time.deltaTime * velocityDissolve;
            //print(fadeValue);

            for (int i = 0; i < P1Materials.Length; i++)
            {
                P1Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
            }

            yield return null;
        }
        yield return null;
    }



    IEnumerator P2Dissappearance()
    {
        while (fadeValue <= maxValue)
        {
            fadeValue += Time.deltaTime * velocityDissolve;
            //print(fadeValue);

            for (int i = 0; i < P2Materials.Length; i++)
            {
                P2Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
            }
            dissolving = true;
            yield return null;
        }
        dissolving = false;
        yield return null;
    }

    IEnumerator P2Appearance()
    {

        while (dissolving)
        {
            yield return null;
        }

        while (fadeValue > minValue)
        {
            fadeValue -= Time.deltaTime * velocityDissolve;
            //print(fadeValue);

            for (int i = 0; i < P2Materials.Length; i++)
            {
                P2Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator characterDisappearance(int player)
    {

        while (fadeValue < maxValue)
        {
            if (!dissolving)
            {
                fadeValue += Time.deltaTime * velocityDissolve;
                print(fadeValue);
                if (player == 1)
                {
                    for (int i = 0; i < P1Materials.Length; i++)
                    {
                        P1Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
                    }
                    yield return null;
                }
                else
                {
                    for (int i = 0; i < P2Materials.Length; i++)
                    {
                        P2Materials[i].SetFloat("Vector1_68C5C62B", fadeValue);
                    }
                    yield return null;
                }
                dissolving = true;
            }
        }
        dissolving = false;
        yield return null;
    }

    public void RestartTimerBall()
    {
        timerBallTime = 0;
    }

    private void RainManager()
    {
        if(TimerScript.matchTime <= timeToStartRaining)
        {
            if(TimerScript.matchTime <= timeToEndRaining)
            {
                PM.EndRainParticles();
            }
            else
            {
                PM.StartRainParticles();

            }
        }
    }
    private void PostProcessingManager()
    {
        if (TimerScript.matchTime <= ppNightMax)
        {
            if (TimerScript.matchTime <= ppNightMin)
            {
                postProcessingNight.SetActive(false);
            }
            else
            {
                postProcessingNight.SetActive(true);

            }
        }
    }

    public void EndCinematic()
    {
        state = stateOfMatch.notStarted;
        PM.StartParticlesP1();
        GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("EnergyConcentration");
        match_started = true;
    }

    private void EnvironmentChanger()
    {
        timer += Time.deltaTime;
        if(timer >= 10f)
        {
            skyBoxChanger.ChangeSkybox();
            timer = 0;
        }
    }
}
