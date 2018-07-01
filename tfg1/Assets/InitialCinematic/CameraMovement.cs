using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float velocityRotation = 1f;
    public float velocityUp = 1f;
    public float velocityBack = 1f;

    public float TimeToChangeScene = 4f;
    public float timer = 0f;

    public LevelChanger LC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= TimeToChangeScene)
        {
            LC.FadeToNextLevel();
        }
        transform.Translate(Vector3.right * Time.deltaTime * velocityRotation);
        target.Translate(Vector3.up * Time.deltaTime * velocityUp);
        transform.Translate(Vector3.up * Time.deltaTime * velocityUp);
        transform.Translate(Vector3.back * Time.deltaTime * velocityBack);
        transform.LookAt(target);

    }

}
