using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public ParticleSystem partP1, partP2;
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
}
