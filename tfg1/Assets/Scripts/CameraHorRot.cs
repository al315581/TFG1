using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorRot : MonoBehaviour {
    public Transform target;

    public float velocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, velocity * Time.deltaTime);

    }
}
