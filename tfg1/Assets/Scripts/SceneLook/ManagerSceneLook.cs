using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSceneLook : MonoBehaviour {

    public int state = 1;
    public GameObject ball;
    public float speedBall = 2f;
    public float timer = 0f;
    public float endPhase1 = 4f;
    public float endPhase2 = 7f;
    public float endPhase3 = 10f;
    public float endPhase4 = 12f;


    // Use this for initialization
    public GameObject P1, P2;
    public float steerVelocity = 2f;
    public float characterSpeed = 1f;
    public float characterSpeed2 = 1f;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        ball.gameObject.transform.position += ball.transform.forward * Time.deltaTime * speedBall;
        switch (state)
        {
            case 1:
                //ball.gameObject.transform.position += ball.transform.forward * Time.deltaTime * speedBall;
                if(timer >= endPhase1)
                {
                    state = 2;
                }
                break;

            case 2:
                P1.GetComponent<Animator>().SetTrigger("mirar");
                P2.GetComponent<Animator>().SetTrigger("mirar");
                if (timer >= endPhase2)
                {
                    state = 3;
                }
                break;

            case 3:

                Vector3 relativePos1 = ball.transform.position - P1.transform.position;
                Quaternion rotation1 = Quaternion.LookRotation(relativePos1);
                P1.transform.rotation = Quaternion.Slerp(P1.transform.rotation, rotation1, Time.deltaTime * steerVelocity);

                Vector3 relativePos2 = ball.transform.position - P1.transform.position;
                Quaternion rotation2 = Quaternion.LookRotation(relativePos1);
                P2.transform.rotation = Quaternion.Slerp(P2.transform.rotation, rotation2, Time.deltaTime * steerVelocity);
                //P2.transform.rotation = Quaternion.Slerp(P2.transform.rotation, rotation, Time.deltaTime * steerVelocity);

                //P2.transform.rotation = rotation;
                P1.GetComponent<Animator>().SetTrigger("run");
                P2.GetComponent<Animator>().SetTrigger("run");

                P1.gameObject.transform.position += P1.gameObject.transform.forward * Time.deltaTime * characterSpeed;
                P2.gameObject.transform.position += P2.gameObject.transform.forward * Time.deltaTime * characterSpeed;

                if (timer >= endPhase3)
                {
                    state = 4;
                }
                break;

            case 4:
                Vector3 relativePos3 = ball.transform.position - P1.transform.position;
                Quaternion rotation3 = Quaternion.LookRotation(relativePos3);
                P1.transform.rotation = Quaternion.Slerp(P1.transform.rotation, rotation3, Time.deltaTime * steerVelocity);

                Vector3 relativePos4 = ball.transform.position - P1.transform.position;
                Quaternion rotation4 = Quaternion.LookRotation(relativePos4);
                P2.transform.rotation = Quaternion.Slerp(P2.transform.rotation, rotation4, Time.deltaTime * steerVelocity);
                /*relativePos = ball.transform.position - transform.position;
                rotation = Quaternion.LookRotation(relativePos);
                P1.transform.rotation = Quaternion.Slerp(P1.transform.rotation, rotation, Time.deltaTime * steerVelocity);
                P2.transform.rotation = Quaternion.Slerp(P2.transform.rotation, rotation, Time.deltaTime * steerVelocity);*/
                P1.gameObject.transform.position += P1.gameObject.transform.forward * Time.deltaTime * characterSpeed2;
                P2.gameObject.transform.position += P2.gameObject.transform.forward * Time.deltaTime * characterSpeed2;


                if (timer >= endPhase4)
                {
                    GameObject.FindObjectOfType<LevelChanger>().FadeToNextLevel();
                }

                break;




        }
	}
}
