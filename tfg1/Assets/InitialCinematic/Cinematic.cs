using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour {
    public Material characterMaterial;
    public Material[] materials;
    // Use this for initialization
    public float fadeValue;
    public float maxValue = 4f;
    public float velocity = 1f;
	void Start () {
        characterMaterial.SetFloat("Vector1_A235A670", -3);
        fadeValue = -3;
        StartCoroutine(characterAppearance());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator characterAppearance()
    {
        while(fadeValue < maxValue)
        {
            fadeValue += Time.deltaTime * velocity;
            //characterMaterial.SetFloat("Vector1_A235A670", fadeValue);
            for(int i = 0; i< materials.Length; i++)
            {
                materials[i].SetFloat("Vector1_A235A670", fadeValue);
            }
            yield return null;
        }

        yield return null;
    }
}
