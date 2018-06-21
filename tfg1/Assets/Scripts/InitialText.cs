using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndCinematic()
    {
        GameObject.FindObjectOfType<GamMan>().EndCinematic();
    }
}
