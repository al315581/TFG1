using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public ParticleSystem partP1, partP2;
    public ParticleSystem hitPlayerPart;
    public GameObject fireTrail;
    public ParticleSystem rain;

    public ParticleSystem burningFieldP1, burningFieldP2;

    public ParticleSystem runParticlesp1, runParticlesp2;
    private bool runPartEmit = false;
    public GameObject runParticlesObject;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void StartParticlesP1()
    {
        partP1.Play();
        
    }

    public void StartParticlesP2()
    {
        partP2.Play();
    }

    public void HitPlayerParticles(Transform t)
    {
        hitPlayerPart.transform.position = new Vector3(t.position.x, 4, t.position.z);
        //print(t.position);
        hitPlayerPart.Play(true);
    }

    public void StartFireTrail()
    {
        if (!fireTrail.activeSelf)
        {
            fireTrail.SetActive(true);
        }
    }
    public void EndFireTrail()
    {
        if (fireTrail.activeSelf)
        {
            fireTrail.SetActive(false);

        }
    }

    public void StartBurningGroundP1()
    {
        burningFieldP1.Play();
    }
    public void StartBurningGroundP2()
    {
        burningFieldP2.Play();
    }

    public void StartRainParticles()
    {
        if(!rain.isEmitting)
        rain.Play();
        //print(rain.isEmitting);
    }

    public void EndRainParticles()
    {
        if(rain.isEmitting)
        rain.Stop();
        //print(rain.isEmitting);
    }

    public void StartRunParticlesP1()
    {
        //print(runParticlesp1.isPlaying);
       // print(runParticlesp1.isEmitting);
        //StartRunParticles2();
         if (!runParticlesp1.isPlaying)
         {
             runParticlesp1.Play();
             //print("damos play");
         }
       /*  
        if (!runPartEmit)
        {
            runParticles.Play();
            runPartEmit = true;
        }*/
    }
    public void EndRunParticlesP1()
    {
        //EndRunParticles2();
        if (runParticlesp1.isPlaying)
        {
            runParticlesp1.Stop();
            //print("las paramos");
        }
        /*
        if (runPartEmit)
        {
            runParticles.Stop();
            runPartEmit = false;
        }*/
    }
    public void StartRunParticlesP2()
    {

        if (!runParticlesp2.isPlaying)
        {
            runParticlesp2.Play();
            //print("damos play");
        }
    }
    public void EndRunParticlesP2()
    {
        if (runParticlesp2.isPlaying)
        {
            runParticlesp2.Stop();
            //print("las paramos");
        }

    }
    /*
    public void StartRunParticles2()
    {
        runParticlesObject.SetActive(true);
    }
    public void EndRunParticles2()
    {
        runParticlesObject.SetActive(false);
    }*/
}
