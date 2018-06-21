using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindObjectOfType<AudioManager>().Play("CinematicSound");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
