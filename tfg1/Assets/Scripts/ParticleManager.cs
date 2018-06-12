﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public ParticleSystem partP1, partP2;
    public ParticleSystem hitPlayerPart;
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
}
