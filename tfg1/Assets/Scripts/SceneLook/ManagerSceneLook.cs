using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSceneLook : MonoBehaviour {

    public int state = 1;
    public GameObject ball;
    public float speedBall = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case 1:
                ball.gameObject.transform.position += ball.transform.forward * Time.deltaTime * speedBall;
                break;

            case 2:
                break;

            case 3:
                break;

            

        }
	}
}
