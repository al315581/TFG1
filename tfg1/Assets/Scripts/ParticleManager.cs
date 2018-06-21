using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public ParticleSystem partP1, partP2;
    public ParticleSystem hitPlayerPart;
    public GameObject fireTrail;
    public ParticleSystem rain;

    public ParticleSystem burningFieldP1, burningFieldP2;
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
        rain.Play();
        print(rain.isEmitting);
    }
}
