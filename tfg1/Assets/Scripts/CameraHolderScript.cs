using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour {

    //public Transform winP1, winP2;
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

    public void startFireWorksP2()
    {
        GameObject.FindObjectOfType<ParticleManager>().StartFireworksRed();
        GameObject.FindObjectOfType<WinnerTextScript>().WinnerTextStart();
        ConfettiP2();
        GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("Fireworks");
    }
    public void startFireWorksP1()
    {
        GameObject.FindObjectOfType<ParticleManager>().StartFireworksBlue();
        GameObject.FindObjectOfType<WinnerTextScript>().WinnerTextStart();
        ConfettiP1();
        GameObject.FindObjectOfType<AudioManager>().PlayRandomPitch("Fireworks");


    }

    public void ConfettiP1()
    {
        GameObject.FindObjectOfType<ParticleManager>().PlayConfettiP1();
    }
    public void ConfettiP2()
    {
        GameObject.FindObjectOfType<ParticleManager>().PlayConfettiP2();

    }
}
