using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScene : MonoBehaviour {
    public GameObject P1, P2;
    public float characterSpeed = 1f;
    public float characterSpeed2 = 1f;
    public float timer = 0f;

    public GameObject ball;
    public float speedBall = 2f;



    public float timeToChange = 6f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ball.gameObject.transform.position += ball.transform.forward * Time.deltaTime * speedBall;
        P1.GetComponent<Animator>().SetTrigger("run");
        P2.GetComponent<Animator>().SetTrigger("run");
        P1.transform.position = new Vector3(P1.transform.position.x, 0, P1.transform.position.z);
        P2.transform.position = new Vector3(P2.transform.position.x, 0, P2.transform.position.z);
        timer += Time.deltaTime;
        P1.gameObject.transform.position += P1.gameObject.transform.forward * Time.deltaTime * characterSpeed2;
        P2.gameObject.transform.position += P2.gameObject.transform.forward * Time.deltaTime * characterSpeed2;


        if(timer >= timeToChange)
        {
            GameObject.FindObjectOfType<LevelChanger>().FadeToNextLevel();
        }
    }
}
