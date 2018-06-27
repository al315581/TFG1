using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour {

    public Transform winP1, winP2;
    Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void MoveToP1()
    {
        anim.SetTrigger("WinP1");
    }
    public void MoveToP2()
    {
        anim.SetTrigger("WinP2");
    }
}
